using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Platform;

namespace Telling.Droid.Controls
{
    [Register("telling.android.controls.TInputValidation")]
    public class TInputValidation : ViewGroup
    {
        public RelativeLayout ParentRelLayout { get; private set; }
        public TextInputLayout TextInputLayoutControl { get; private set; }
        public TextInputEditText EditTextControl { get; private set; }
        public TextView TextViewError { get; private set; }
        public View DividerLine { get; private set; }
        public Drawable DrawableRight { get; set; }

        public string Text
        {
            get { return EditTextControl?.Text; }
            set
            {
                EditTextControl.Text = value;
                UpdateLabelSize(value?.Length > 0);
            }
        }

        public string ErrorText { get; set; }
        public string TextHint { get; set; }
        public ColorStateList TextColor { get; set; }
        public ColorStateList TextHintColor { get; set; }
        public Color DividerColor { get; set; }
        public Color DividerColorFocus { get; set; }
        public float EditTextSize { get; set; }
        public float ErrorTextSize { get; set; }
        public float LabelTextSize { get; set; }
        public InputTypes InputType { get; set; }
        public ImeAction ImeOptions { get; set; }
        public int TextInputPadding { get; set; }
        public string Mask { get; set; }

        public event EventHandler<TextChangedEventArgs> TextChanged;

        int _padding2dp;
        int _padding3dp;
        int _padding7dp;
        int _padding10dp;

        public TInputValidation(Context context)
            : base(context)
        {
        }

        public TInputValidation(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
        }

        public TInputValidation(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {

            _padding2dp = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 2, Resources.DisplayMetrics);
            _padding3dp = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 3, Resources.DisplayMetrics);
            _padding7dp = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 7, Resources.DisplayMetrics);
            _padding10dp = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 10, Resources.DisplayMetrics);

            // Load defaults from resources
            var defaultEditTextSize = Resources.GetDimension(Resource.Dimension.text_medium);
            var defaultEditTextLabelSize = Resources.GetDimension(Resource.Dimension.text_medium);
            var defaultErrorTextSize = Resources.GetDimension(Resource.Dimension.text_medium);

            // Retrieve styles attributes
            var controlAttrs = context.ObtainStyledAttributes(attrs, Resource.Styleable.EditTextValidate, defStyleAttr, Resource.Style.EditTextValidationStyle);

            TextHint = controlAttrs.GetString(Resource.Styleable.EditTextValidate_android_hint);
            TextColor = controlAttrs.GetColorStateList(Resource.Styleable.EditTextValidate_android_textColor);
            TextHintColor = controlAttrs.GetColorStateList(Resource.Styleable.EditTextValidate_android_textColorHint);
            DividerColor = controlAttrs.GetColor(Resource.Styleable.EditTextValidate_dividerColor, ContextCompat.GetColor(context, Resource.Color.divider));
            DividerColorFocus = controlAttrs.GetColor(Resource.Styleable.EditTextValidate_dividerColorFocus, ContextCompat.GetColor(context, Resource.Color.dividerFocused));
            EditTextSize = controlAttrs.GetDimension(Resource.Styleable.EditTextValidate_android_textSize, defaultEditTextSize);
            ErrorTextSize = controlAttrs.GetDimension(Resource.Styleable.EditTextValidate_errorTextSize, defaultErrorTextSize);
            LabelTextSize = controlAttrs.GetDimension(Resource.Styleable.EditTextValidate_labelTextSize, defaultEditTextLabelSize);
            InputType = (InputTypes)controlAttrs.GetInt(Resource.Styleable.EditTextValidate_android_inputType, (int)InputTypes.ClassText);
            ImeOptions = (ImeAction)controlAttrs.GetInt(Resource.Styleable.EditTextValidate_android_imeOptions, (int)InputTypes.ClassText);
            TextInputPadding = (int)controlAttrs.GetDimension(Resource.Styleable.EditTextValidate_textInputPadding, 0);
            Mask = controlAttrs.GetString(Resource.Styleable.EditTextValidate_mask);

            DrawableRight = controlAttrs.GetDrawable(Resource.Styleable.EditTextValidate_android_drawableRight);

            controlAttrs.Recycle();

            Init();
        }

        public TInputValidation(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected TInputValidation(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            var parentLayout = (RelativeLayout)GetChildAt(0);
            parentLayout.Layout(0, 0, r, b);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            var parentLayout = GetChildAt(0) as RelativeLayout;
            var inputLayout = parentLayout.GetChildAt(0) as TextInputLayout;

            var height = inputLayout.MeasuredHeight;
            parentLayout.Measure(widthMeasureSpec, height);

            SetMeasuredDimension(widthMeasureSpec, height);
        }

        void Init()
        {
            var parentRelLayout = CreateParentLayout();

            parentRelLayout.AddView(CreateTextInputLayoutControl());

            AddView(parentRelLayout);
        }

        RelativeLayout CreateParentLayout()
        {
            // Parent layout
            ParentRelLayout = new RelativeLayout(Context);
            var lp = new RelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
            ParentRelLayout.LayoutParameters = lp;

            return ParentRelLayout;
        }

        TextInputLayout CreateTextInputLayoutControl()
        {
            // TextInputLayout
            TextInputLayoutControl = new TextInputLayout(Context);
            var lpTextInputLayout = new RelativeLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            lpTextInputLayout.AddRule(LayoutRules.AlignParentLeft);
            TextInputLayoutControl.LayoutParameters = lpTextInputLayout;
            TextInputLayoutControl.SetPadding(TextInputPadding, 0, TextInputPadding, 0);
            SetTextInputLayoutControlColor(TextInputLayoutControl, TextHintColor);

            // Add EditText
            TextInputLayoutControl.AddView(CreateEditText(), 0);

            // Add Divider
            TextInputLayoutControl.AddView(CreateDivider(), 1);

            // Add Error Label
            TextInputLayoutControl.AddView(CreateErrorTextLabel(), 2);

            return TextInputLayoutControl;
        }

        EditText CreateEditText()
        {
            EditTextControl = string.IsNullOrEmpty(Mask) ? new TextInputEditText(Context) : new MaskedEditText(Context, Mask);

            var lpEditText = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

            EditTextControl.LayoutParameters = lpEditText;
            EditTextControl.Hint = TextHint;
            EditTextControl.Text = Text;
            EditTextControl.ImeOptions = ImeOptions;
            EditTextControl.SetTextColor(TextColor ?? ContextCompat.GetColorStateList(Context, Resource.Color.text));
            EditTextControl.SetHintTextColor(TextHintColor ?? ContextCompat.GetColorStateList(Context, Resource.Color.text));
            EditTextControl.Background = null;
            EditTextControl.InputType = InputType;
            EditTextControl.ImeOptions = ImeAction.Done;
            EditTextControl.FocusChange += EditText_FocusChange;
            EditTextControl.TextChanged += EditText_TextChanged;

            UpdateLabelSize(Text?.Length > 0);

            EditTextControl.SetCompoundDrawablesWithIntrinsicBounds(null, null, DrawableRight, null);

            return EditTextControl;
        }

        View CreateDivider()
        {
            DividerLine = new View(Context);

            var lpDivider = new LinearLayout.LayoutParams(LayoutParams.MatchParent, (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 2, Resources.DisplayMetrics));

            DividerLine.LayoutParameters = lpDivider;
            DividerLine.SetBackgroundColor(new Color(DividerColor.ToArgb()));

            return DividerLine;
        }

        TextView CreateErrorTextLabel()
        {
            TextViewError = new TextView(Context);

            var lpTextViewError = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);

            TextViewError.LayoutParameters = lpTextViewError;
            TextViewError.SetPadding(0, _padding2dp, 0, 0);
            TextViewError.SetTextColor(ContextCompat.GetColorStateList(Context, Resource.Color.validation));
            TextViewError.SetTextSize(ComplexUnitType.Px, ErrorTextSize);
            TextViewError.Visibility = ViewStates.Visible;

            return TextViewError;
        }

        public void Validate()
        {
            if (TextViewError != null)
            {
                TextViewError.Text = ErrorText;

                if (string.IsNullOrWhiteSpace(ErrorText))
                {
                    TextViewError.Visibility = ViewStates.Invisible;
                    DividerLine.SetBackgroundColor(new Color(ContextCompat.GetColor(Context, Resource.Color.divider)));
                }
                else
                {
                    TextViewError.Visibility = ViewStates.Visible;
                    DividerLine.SetBackgroundColor(new Color(ContextCompat.GetColor(Context, Resource.Color.validation)));
                }
            }
        }

        void EditText_FocusChange(object sender, FocusChangeEventArgs e)
        {
            var mgr = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);

            if (TextViewError.Visibility == ViewStates.Invisible)
            {
                if (e.HasFocus)
                    DividerLine.SetBackgroundColor(new Color(DividerColorFocus.ToArgb()));
                else
                    DividerLine.SetBackgroundColor(new Color(DividerColor.ToArgb()));
            }

            if (e.HasFocus)
            {
                mgr.ShowSoftInput(EditTextControl, ShowFlags.Implicit);
                UpdateLabelSize(e.HasFocus);
            }
            else
            {
                Validate();
            }
        }

        void EditText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var showLabel = e.AfterCount < 0;
            UpdateLabelSize(!showLabel);

            TextChanged?.Invoke(this, e);
        }

        void UpdateLabelSize(bool showHint)
        {
            //if (showHint)
            //{
            //    EditTextControl.SetTextSize(ComplexUnitType.Px, EditTextSize);
            //    EditTextControl.SetPadding(0, _padding7dp, 0, _padding7dp);
            //}
            //else
            //{
            EditTextControl.SetTextSize(ComplexUnitType.Px, LabelTextSize);
            EditTextControl.SetPadding(0, _padding10dp, 0, _padding10dp);
            //}
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && EditTextControl != null)
            {
                EditTextControl.FocusChange -= EditText_FocusChange;
                EditTextControl.TextChanged -= EditText_TextChanged;
            }

            base.Dispose(disposing);
        }

        private static void SetTextInputLayoutControlColor(TextInputLayout textInputLayoutControl, ColorStateList textHintColor)
        {
            try
            {
                var defaultTextColor = Java.Lang.Class.FromType(typeof(TextInputLayout)).GetDeclaredField("mDefaultTextColor");
                defaultTextColor.Accessible = true;
                defaultTextColor.Set(textInputLayoutControl, textHintColor);
                var focusedTextColor = Java.Lang.Class.FromType(typeof(TextInputLayout)).GetDeclaredField("mFocusedTextColor");
                focusedTextColor.Accessible = true;
                focusedTextColor.Set(textInputLayoutControl, textHintColor);
            }
            catch (Exception e)
            {
                Mvx.Warning($"Failed to use reflection to set the hint color :{e.Message}");
            }
        }
    }
}