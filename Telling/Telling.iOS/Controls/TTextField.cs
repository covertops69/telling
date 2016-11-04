using Cirrious.FluentLayouts.Touch;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using Telling.iOS.Helpers;
using UIKit;

namespace Telling.iOS.Controls
{
    //public class TTextField : UITextField
    //{
    //    bool _initialized = false;

    //    //public ValidationView ValidationView { get; set; }

    //    public UITextField NextTextField { get; set; }

    //    public bool IsRequired { get; set; }

    //    public bool HasTooltip { get; set; }

    //    public string TooltipTitle { get; set; }

    //    public string TooltipBody { get; set; }

    //    public bool IsPicker { get; set; }

    //    public string ErrorText { get; set; }

    //    public nfloat TopPadding { get; set; }

    //    bool _isEditable;
    //    public bool IsEditable
    //    {
    //        get
    //        {
    //            return _isEditable;
    //        }
    //        set
    //        {
    //            if (IsEditable != value)
    //            {
    //                _isEditable = value;

    //                ShouldBeginEditing = t => {
    //                    return value;
    //                };
    //            }
    //        }
    //    }

    //    public TTextField()
    //    {
    //        ContentMode = UIViewContentMode.Redraw;

    //        TopPadding = 0f;
    //        IsEditable = true;
    //        TextColor = UIColor.White;

    //        TranslatesAutoresizingMaskIntoConstraints = false;

    //        EditingDidBegin += HandleEditingDidBegin;
    //        EditingDidEnd += HandleEditingDidEnd;

    //        ShouldReturn += HandleShouldReturn;
    //    }

    //    bool HandleShouldReturn(UITextField textField)
    //    {
    //        var tf = textField as TTextField;

    //        if (tf != null && tf.NextTextField != null)
    //        {
    //            tf.NextTextField.BecomeFirstResponder();
    //        }

    //        tf.ResignFirstResponder();

    //        return true;
    //    }

    //    void HandleEditingDidBegin(object sender, EventArgs e)
    //    {
    //        NSNotificationCenter.DefaultCenter.PostNotificationName(NotificationCenterConstants.TextFieldEditingDidBegin, sender as NSObject);
    //    }

    //    void HandleEditingDidEnd(object sender, EventArgs e)
    //    {
    //        var textfield = sender as TTextField;

    //        textfield.Validate();
    //    }

    //    public override void Draw(CGRect rect)
    //    {
    //        Initialize();

    //        if (IsRequired && HasTooltip)
    //        {
    //            RightViewMode = UITextFieldViewMode.Always;

    //            var label = new UILabel(new CGRect(0, 7, 10, 40))
    //            {
    //                Text = "*",
    //                TextAlignment = UITextAlignment.Right
    //            };

    //            //var questionMark = new TImageButton("Images/helpIcon.png");

    //            //questionMark.TouchUpInside += (sender, e) => {
    //            //    ShowModalPopup(TooltipTitle, TooltipBody);
    //            //};

    //            var rightView = new UIView(new CGRect(0, 0, 30, 50));

    //            rightView.Add(label);
    //            //rightView.Add(questionMark);

    //            rightView.AddConstraints(new FluentLayout[] {
    //                label.AtTopOf(rightView),
    //                label.AtLeftOf(rightView),
    //                label.AtBottomOf(rightView),

    //                //questionMark.AtTopOf(rightView),
    //                //questionMark.ToRightOf(label),
    //                //questionMark.AtRightOf(rightView),
    //                //questionMark.AtBottomOf(rightView)
    //            });

    //            RightView = rightView;
    //        }
    //        else if (IsRequired)
    //        {
    //            RightViewMode = UITextFieldViewMode.Always;

    //            var label = new UILabel(new CGRect(0, 0, 10, 40))
    //            {
    //                Text = "*",
    //                TextAlignment = UITextAlignment.Right
    //            };

    //            RightView = label;
    //        }
    //        else if (HasTooltip)
    //        {
    //            RightViewMode = UITextFieldViewMode.Always;

    //            //var questionMark = new TImageButton("Images/helpIcon.png")
    //            //{
    //            //    Frame = new CGRect(0, 0, 40, 40)
    //            //};

    //            //questionMark.TouchUpInside += (sender, e) =>
    //            //{
    //            //    ShowModalPopup(TooltipTitle, TooltipBody);
    //            //};

    //            //RightView = questionMark;
    //        }
    //        else if (IsPicker)
    //        {
    //            RightViewMode = UITextFieldViewMode.Always;

    //            var dropdownImage = new UIImageView(UIImage.FromBundle("Images/down_section_arrow_blue.png"))
    //            {
    //                Frame = new CGRect(0, 0, 12, 40),
    //                ContentMode = UIViewContentMode.ScaleAspectFit
    //            };

    //            RightView = dropdownImage;
    //        }

    //        //if (Enabled)
    //        //{
    //        //    AttributedPlaceholder = new NSAttributedString(string.IsNullOrEmpty(Placeholder) ? "" : Placeholder, null, ColorPalette.Grey);
    //        //}
    //        //else
    //        //{
    //        //    AttributedPlaceholder = new NSAttributedString(string.IsNullOrEmpty(Placeholder) ? "" : Placeholder, null, ColorPalette.GreyLight);
    //        //}

    //        base.Draw(rect);
    //    }

    //    public void Validate()
    //    {
    //        //if (ValidationView != null)
    //        //{
    //        //    ValidationView.ErrorText = ErrorText;
    //        //    ValidationView.Hidden = string.IsNullOrWhiteSpace(ErrorText);
    //        //}
    //    }

    //    void Initialize()
    //    {
    //        if (!_initialized)
    //        {
    //            var border = new CALayer();
    //            nfloat width = 1;

    //            border.BorderColor = ColorPalette.DarkPumpkin.CGColor;
    //            border.BorderWidth = width;
    //            border.Frame = new CGRect(x: 0, y: Frame.Size.Height - width, width: Frame.Size.Width, height: Frame.Size.Height);

    //            Layer.AddSublayer(border);
    //            Layer.MasksToBounds = true;

    //            _initialized = true;
    //        }
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            ShouldReturn -= HandleShouldReturn;
    //            EditingDidBegin -= HandleEditingDidBegin;
    //            EditingDidEnd -= HandleEditingDidEnd;
    //        }

    //        base.Dispose(disposing);
    //    }
    //}
}