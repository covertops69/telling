using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace Telling.Droid.Controls
{
    [Register("telling.droid.controls.TTextField")]
    public class TTextField : ViewGroup
    {
        public RelativeLayout ParentRelLayout { get; private set; }
        public TextInputLayout TextInputLayoutControl { get; private set; }
        public TextInputEditText EditTextControl { get; private set; }
        public TextView TextViewError { get; private set; }
        public View DividerLine { get; private set; }

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
        public float EditTextSize { get; set; }
        public float ErrorTextSize { get; set; }
        public float LabelTextSize { get; set; }
        public InputTypes InputType { get; set; }
        public ImeAction ImeOptions { get; set; }

        public event EventHandler TextChanged;

        int _padding2dp;
        int _padding3dp;
        int _padding7dp;
        int _padding10dp;

        public TTextField(Context context)
            : base(context)
        {
        }

        public TTextField(Context context, IAttributeSet attrs)
            : this(context, attrs, 0)
        {
        }

        public TTextField(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
            // Load defaults from resources
            var defaultEditTextSize = Resources.GetDimension(Resource.Dimension.default_edit_text_validation_edit_textsize);
            var defaultEditTextLabelSize = Resources.GetDimension(Resource.Dimension.default_edit_text_validation_label_textsize);
            var defaultErrorTextSize = Resources.GetDimension(Resource.Dimension.default_edit_text_validation_error_textsize);

            // Retrieve styles attributes
            var controlAttrs = context.ObtainStyledAttributes(attrs, Resource.Styleable.EditTextValidate, defStyleAttr, Resource.Style.EditTextValidationStyle);

            TextHint = controlAttrs.GetString(Resource.Styleable.EditTextValidate_android_hint);
            EditTextSize = controlAttrs.GetDimension(Resource.Styleable.EditTextValidate_android_textSize, defaultEditTextSize);
            ErrorTextSize = controlAttrs.GetDimension(Resource.Styleable.EditTextValidate_errorTextSize, defaultErrorTextSize);
            LabelTextSize = controlAttrs.GetDimension(Resource.Styleable.EditTextValidate_labelTextSize, defaultEditTextLabelSize);
            InputType = (InputTypes)controlAttrs.GetInt(Resource.Styleable.EditTextValidate_android_inputType, (int)InputTypes.ClassText);
            ImeOptions = (ImeAction)controlAttrs.GetInt(Resource.Styleable.EditTextValidate_android_imeOptions, (int)InputTypes.ClassText);

            controlAttrs.Recycle();

            _padding2dp = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 2, Resources.DisplayMetrics);
            _padding3dp = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 3, Resources.DisplayMetrics);
            _padding7dp = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 7, Resources.DisplayMetrics);
            _padding10dp = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 10, Resources.DisplayMetrics);

            Init();
        }

        public TTextField(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        protected TTextField(IntPtr javaReference, JniHandleOwnership transfer)
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
            TextInputLayoutControl.SetPadding(_padding7dp, 0, _padding7dp, 0);

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
            EditTextControl = new TextInputEditText(Context);
            var lpEditText = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);
            EditTextControl.LayoutParameters = lpEditText;
            EditTextControl.Hint = TextHint;
            EditTextControl.Text = Text;
            EditTextControl.ImeOptions = ImeOptions;
            EditTextControl.SetTextColor(ContextCompat.GetColorStateList(Context, Resource.Color.deep_lemon));
            EditTextControl.SetHintTextColor(ContextCompat.GetColorStateList(Context, Resource.Color.deep_lemon));
            EditTextControl.Background = null;
            EditTextControl.InputType = InputType;
            EditTextControl.ImeOptions = ImeAction.Done;
            EditTextControl.FocusChange += EditText_FocusChange;
            EditTextControl.TextChanged += EditText_TextChanged;

            UpdateLabelSize(Text?.Length > 0);

            return EditTextControl;
        }

        View CreateDivider()
        {
            DividerLine = new View(Context);
            var lpDivider = new LinearLayout.LayoutParams(LayoutParams.MatchParent, (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, 2, Resources.DisplayMetrics));
            DividerLine.LayoutParameters = lpDivider;
            DividerLine.SetBackgroundColor(new Color(ContextCompat.GetColor(Context, Resource.Color.deep_lemon)));

            return DividerLine;
        }

        TextView CreateErrorTextLabel()
        {
            TextViewError = new TextView(Context);
            var lpTextViewError = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
            TextViewError.LayoutParameters = lpTextViewError;
            TextViewError.SetPadding(0, _padding2dp, 0, 0);
            TextViewError.SetTextColor(ContextCompat.GetColorStateList(Context, Resource.Color.deep_lemon));
            TextViewError.SetTextSize(ComplexUnitType.Px, ErrorTextSize);
            TextViewError.Visibility = ViewStates.Invisible;

            return TextViewError;
        }

        public void Validate()
        {
            TextViewError.Text = ErrorText;

            if (string.IsNullOrWhiteSpace(ErrorText))
            {
                TextViewError.Visibility = ViewStates.Invisible;
                DividerLine.SetBackgroundColor(new Color(ContextCompat.GetColor(Context, Resource.Color.deep_lemon)));
            }
            else
            {
                TextViewError.Visibility = ViewStates.Visible;
                DividerLine.SetBackgroundColor(new Color(ContextCompat.GetColor(Context, Resource.Color.deep_lemon)));
            }
        }

        void EditText_FocusChange(object sender, FocusChangeEventArgs e)
        {
            var mgr = (InputMethodManager)Context.GetSystemService(Context.InputMethodService);

            if (string.IsNullOrEmpty(ErrorText))
            {
                if (e.HasFocus)
                    DividerLine.SetBackgroundColor(new Color(ContextCompat.GetColor(Context, Resource.Color.deep_lemon)));
                else
                    DividerLine.SetBackgroundColor(new Color(ContextCompat.GetColor(Context, Resource.Color.deep_lemon)));
            }
            else
            {
                DividerLine.SetBackgroundColor(new Color(ContextCompat.GetColor(Context, Resource.Color.deep_lemon)));
            }

            if (e.HasFocus)
            {
                mgr.ShowSoftInput(EditTextControl, ShowFlags.Implicit);
                UpdateLabelSize(e.HasFocus);
            }
        }

        void EditText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var showLabel = e.AfterCount < 0;
            UpdateLabelSize(!showLabel);

            TextChanged?.Invoke(this, EventArgs.Empty);
        }

        void UpdateLabelSize(bool showHint)
        {
            if (showHint)
            {
                EditTextControl.SetTextSize(ComplexUnitType.Px, EditTextSize);
                EditTextControl.SetPadding(0, _padding7dp, 0, _padding7dp);
            }
            else
            {
                EditTextControl.SetTextSize(ComplexUnitType.Px, LabelTextSize);
                EditTextControl.SetPadding(0, _padding10dp, 0, _padding10dp);
            }
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
    }
}