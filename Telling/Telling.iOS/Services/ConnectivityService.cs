using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Telling.Core.Interfaces;
using Telling.Core.Services;
using Telling.iOS.Services;

namespace Telling.iOS.Services
{
    public class ConnectivityService : BaseConnectivityService
    {
        public ConnectivityService()
        {
            UpdateConnected(true);
            Reachability.ReachabilityChanged += ReachabilityChanged;
        }

        void ReachabilityChanged(object sender, EventArgs e)
        {
            UpdateConnected();
        }

        bool isConnected;

        NetworkStatus previousInternetStatus = NetworkStatus.NotReachable;

        void UpdateConnected(bool triggerChange = true)
        {
            var remoteHostStatus = Reachability.RemoteHostStatus();
            var internetStatus = Reachability.InternetConnectionStatus();
            var localWifiStatus = Reachability.LocalWifiConnectionStatus();

            var previouslyConnected = isConnected;
            isConnected = (internetStatus == NetworkStatus.ReachableViaCarrierDataNetwork ||
                internetStatus == NetworkStatus.ReachableViaWiFiNetwork) ||
                (localWifiStatus == NetworkStatus.ReachableViaCarrierDataNetwork ||
                    localWifiStatus == NetworkStatus.ReachableViaWiFiNetwork) ||
                (remoteHostStatus == NetworkStatus.ReachableViaCarrierDataNetwork ||
                    remoteHostStatus == NetworkStatus.ReachableViaWiFiNetwork);

            if (triggerChange && (previouslyConnected != isConnected || previousInternetStatus != internetStatus))
                OnConnectivityChanged(new ConnectivityChangedEventArgs { IsConnected = isConnected });
            previousInternetStatus = internetStatus;

            // CheckNotificationRegister();
        }

        //private async void CheckNotificationRegister()
        //{
        //    var settings = Mvx.Resolve<ISettings>();
        //    var token = settings.GetValue<string>(pcl.Constants.SETTING_AWS_DEVICE_TOKEN) ?? null;
        //    var deviceType = settings.GetValue<string>(pcl.Constants.SETTING_DEVICE_TYPE) ?? null;
        //    var registered = settings.GetValue<bool>(pcl.Constants.SETTING_IS_REGISTERING_FOR_PUSH);
        //    if (!registered && !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(deviceType))
        //    {
        //        var _snsClient = new AmazonSimpleNotificationServiceClient(pcl.Constants.AWS_ACCESS_KEY_ID, pcl.Constants.AWS_SECRET_ACCESS_KEY, RegionEndpoint.USEast1);
        //        settings.AddOrUpdateValue(pcl.Constants.SETTING_IS_REGISTERING_FOR_PUSH, true);
        //        try
        //        {
        //            // Register with SNS to create an endpoint ARN
        //            var response = await _snsClient.CreatePlatformEndpointAsync(
        //                new CreatePlatformEndpointRequest
        //                {
        //                    Token = token,
        //                    CustomUserData = deviceType,
        //                    PlatformApplicationArn = pcl.Constants.AWS_IOS_ARN
        //                });

        //            settings.AddOrUpdateValue(pcl.Constants.SETTING_AWS_ENDPOINT_ARN, response.EndpointArn);
        //            Debug.WriteLine($"ARN: {response.EndpointArn}");
        //        }
        //        catch (Exception ex)
        //        {
        //            settings.AddOrUpdateValue(pcl.Constants.SETTING_IS_REGISTERING_FOR_PUSH, false);
        //        }
        //    }
        //}
        /// <summary>
        /// Gets if there is an active internet connection
        /// </summary>
        public override bool IsConnected { get { return isConnected; } }

        /// <summary>
        /// Tests if a host name is pingable
        /// </summary>
        /// <param name="host">The host name can either be a machine name, such as "java.sun.com", or a textual representation of its IP address (127.0.0.1)</param>
        /// <param name="msTimeout">Timeout in milliseconds</param>
        /// <returns></returns>
        public override async Task<bool> IsReachable(string host, int msTimeout = 5000)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host));

            if (!IsConnected)
                return false;

            return await IsRemoteReachable(host, 80, msTimeout);
        }

        /// <summary>
        /// Tests if a remote host name is reachable 
        /// </summary>
        /// <param name="host">Host name can be a remote IP or URL of website (no http:// or www.</param>
        /// <param name="port">Port to attempt to check is reachable.</param>
        /// <param name="msTimeout">Timeout in milliseconds.</param>
        /// <returns></returns>
        public override async Task<bool> IsRemoteReachable(string host, int port = 80, int msTimeout = 5000)
        {
            if (string.IsNullOrEmpty(host))
                throw new ArgumentNullException(nameof(host));

            if (!IsConnected)
                return false;

            host = host.Replace("http://www.", string.Empty).
                Replace("http://", string.Empty).
                Replace("https://www.", string.Empty).
                Replace("https://", string.Empty);

            return await Task.Run(() =>
            {
                try
                {
                    var clientDone = new ManualResetEvent(false);
                    var reachable = false;
                    var hostEntry = new DnsEndPoint(host, port);
                    using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                    {
                        var socketEventArg = new SocketAsyncEventArgs { RemoteEndPoint = hostEntry };
                        socketEventArg.Completed += (s, e) =>
                        {
                            reachable = e.SocketError == SocketError.Success;

                            clientDone.Set();
                        };

                        clientDone.Reset();

                        socket.ConnectAsync(socketEventArg);

                        clientDone.WaitOne(msTimeout);

                        return reachable;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Unable to reach: " + host + " Error: " + ex);
                    return false;
                }
            });
        }

        /// <summary>
        /// Gets the list of all active connection types.
        /// </summary>
        public override IEnumerable<ConnectionType> ConnectionTypes
        {
            get
            {
                var status = Reachability.InternetConnectionStatus();
                switch (status)
                {
                    case NetworkStatus.ReachableViaCarrierDataNetwork:
                        yield return ConnectionType.Cellular;
                        break;
                    case NetworkStatus.ReachableViaWiFiNetwork:
                        yield return ConnectionType.WiFi;
                        break;
                    default:
                        yield return ConnectionType.Other;
                        break;
                }
            }
        }

        /// <summary>
        /// Not supported on iOS
        /// </summary>
        public override IEnumerable<UInt64> Bandwidths
        {
            get { return new UInt64[] { }; }
        }

        bool disposed;

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        public override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Reachability.ReachabilityChanged -= ReachabilityChanged;
                    Reachability.Dispose();
                }

                disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}