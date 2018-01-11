using System;
using Android.Content;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;

namespace Telling.Droid.Controls
{
    public class MaskedEditText : TextInputEditText, View.IOnFocusChangeListener, ITextWatcher, TextView.IOnEditorActionListener
    {
        #region Constructors and Destructors

        public MaskedEditText(Context context)
            : base(context)
        {
        }

        public MaskedEditText(Context context, string mask, char maskFill = ' ', char representation = '#')
           : base(context)
        {
            AddTextChangedListener(this);

            _mask = mask;
            _maskFill = maskFill;
            _representation = representation;

            CleanUp();

            SetOnEditorActionListener(this);
        }

        public MaskedEditText(Context context, IAttributeSet attrs)
            : base(context, attrs)
                => Initialize(attrs);

        public MaskedEditText(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
                => Initialize(attrs);

        protected MaskedEditText(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        #endregion

        #region Fields

        int _selection, _maxRawLength, _lastValidMaskPosition;
        char _representation, _maskFill;
        bool _ignore, _initialized, _editingAfter, _editingBefore, _editingOnChanged, _selectionChanged;
        int[] _rawToMask, _maskToRaw;
        char[] _charsInMask;
        string _mask;
        RawText _rawText = new RawText();
        IOnFocusChangeListener _focusChangeListener;

        #endregion

        #region Public Properties

        public string Mask
        {
            get => _mask;
            set
            {
                _mask = value;
                CleanUp();
            }
        }

        char Representation
        {
            get => _representation;
            set
            {
                _representation = value;
                CleanUp();
            }
        }

        public override IOnFocusChangeListener OnFocusChangeListener
        {
            get => base.OnFocusChangeListener;
            set => _focusChangeListener = value;
        }

        #endregion

        #region Public Methods and Operators

        public void OnFocusChange(View v, bool hasFocus)
        {
            if (_focusChangeListener != null)
                _focusChangeListener.OnFocusChange(v, hasFocus);

            if (HasFocus && (_rawText.Length > 0 || !HasHint))
            {
                _selectionChanged = false;
                SetSelection(LastValidPosition());
            }
        }

        public bool OnEditorAction(TextView v, [GeneratedEnum] ImeAction actionId, KeyEvent e)
            => actionId != ImeAction.Next;

        #endregion

        #region Properties

        bool HasHint
            => Hint != null;

        #endregion

        #region Methods

        void Initialize(IAttributeSet attrs)
        {
            var styledAttributes = Context.ObtainStyledAttributes(attrs, Resource.Styleable.MaskedEditText);
            var count = styledAttributes.IndexCount;

            for (var i = 0; i < count; ++i)
            {
                var attr = styledAttributes.GetIndex(i);
                if (attr == Resource.Styleable.MaskedEditText_Mask)
                {
                    AddTextChangedListener(this);

                    _mask = (styledAttributes.GetString(attr) ?? _mask);
                    _maskFill = (styledAttributes.GetString(Resource.Styleable.MaskedEditText_MaskFill) ?? " ")[0];
                    _representation = (styledAttributes.GetString(Resource.Styleable.MaskedEditText_CharRepresentation) ?? "#")[0];

                    CleanUp();

                    SetOnEditorActionListener(this);
                }
            }

            styledAttributes.Recycle();
        }

        void CleanUp()
        {
            if (Mask != null)
            {
                _initialized = false;

                GeneratePositionArrays();

                _rawText = new RawText();

                _selection = _rawToMask[0];

                _editingBefore = true;
                _editingOnChanged = true;
                _editingAfter = true;

                Text = HasHint ? null : Mask.Replace(_representation, _maskFill);

                _editingBefore = false;
                _editingOnChanged = false;
                _editingAfter = false;

                _maxRawLength = _maskToRaw[PreviousValidPosition(Mask.Length - 1)] + 1;
                _lastValidMaskPosition = FindLastValidMaskPosition();
                _initialized = true;

                base.OnFocusChangeListener = this;
            }
        }

        int FindLastValidMaskPosition()
        {
            for (int i = _maskToRaw.Length - 1; i >= 0; i--)
            {
                if (_maskToRaw[i] != -1)
                    return i;
            }

#pragma warning disable CC0022 // Should dispose object
            throw new RuntimeException("Mask contains only the representation char");
#pragma warning restore CC0022 // Should dispose object
        }

        void GeneratePositionArrays()
        {
            var aux = new int[Mask.Length];
            _maskToRaw = new int[Mask.Length];
            var charsInMaskAux = "";

            var charIndex = 0;
            using (var builder = new StringBuilder())
            {
                builder.Append(charsInMaskAux);
                for (int i = 0; i < Mask.Length; i++)
                {
                    var currentChar = Mask[i];
                    if (currentChar == _representation)
                    {
                        aux[charIndex] = i;
                        _maskToRaw[i] = charIndex++;
                    }
                    else
                    {
                        var charAsString = currentChar.ToString();
                        if (!charsInMaskAux.Contains(charAsString) && !Character.IsLetter(currentChar) && !Character.IsDigit(currentChar))
                            builder.Append(charAsString);

                        _maskToRaw[i] = -1;
                    }
                }

                charsInMaskAux = builder.ToString();

                if (charsInMaskAux.IndexOf(' ') < 0)
                    charsInMaskAux = charsInMaskAux + " ";

                _charsInMask = charsInMaskAux.ToCharArray();

                _rawToMask = new int[charIndex];
                for (int i = 0; i < charIndex; i++)
                    _rawToMask[i] = aux[i];
            }
        }

        int ErasingStart(int start)
        {
            while (start > 0 && _maskToRaw[start] == -1)
                start--;

            return start;
        }

        protected override void OnSelectionChanged(int selStart, int selEnd)
        {
            // On Android 4+ this method is being called more than 1 time if there is a hint in the EditText, what moves the cursor to left
            // Using the boolean var selectionChanged to limit to one execution
            if (Mask == null)
            {
                base.OnSelectionChanged(selStart, selEnd);
                return;
            }

            if (_initialized)
            {
                if (!_selectionChanged)
                {

                    if (_rawText.Length == 0 && HasHint)
                    {
                        selStart = 0;
                        selEnd = 0;
                    }
                    else
                    {
                        selStart = FixSelection(selStart);
                        selEnd = FixSelection(selEnd);
                    }

                    SetSelection(selStart, selEnd);
                    _selectionChanged = true;
                }
                else
                {
                    //check to see if the current selection is outside the already entered text
                    if (!(HasHint && _rawText.Length == 0) && selStart > _rawText.Length - 1)
                        SetSelection(FixSelection(selStart), FixSelection(selEnd));
                }
            }

            base.OnSelectionChanged(selStart, selEnd);
        }

        int FixSelection(int selection)
        {
            var lastValidPosition = LastValidPosition();

            return selection > lastValidPosition
                ? lastValidPosition
                : NextValidPosition(selection);
        }

        int NextValidPosition(int currentPosition)
        {
            while (currentPosition < _lastValidMaskPosition && _maskToRaw[currentPosition] == -1)
                currentPosition++;

            if (currentPosition > _lastValidMaskPosition)
                return _lastValidMaskPosition + 1;

            return currentPosition;
        }

        int PreviousValidPosition(int currentPosition)
        {
            while (currentPosition >= 0 && _maskToRaw[currentPosition] == -1)
            {
                currentPosition--;
                if (currentPosition < 0)
                    return NextValidPosition(0);
            }

            return currentPosition;
        }

        int LastValidPosition()
        {
            if (_rawText.Length == _maxRawLength)
                return _rawToMask[_rawText.Length - 1] + 1;

            return NextValidPosition(_rawToMask[_rawText.Length]);
        }

        string MakeMaskedText()
        {
            var maskedText = Mask.Replace(_representation, ' ').ToCharArray();

            for (int i = 0; i < _rawToMask.Length; i++)
            {
                if (i < _rawText.Length)
                    maskedText[_rawToMask[i]] = _rawText[i];
                else
                    maskedText[_rawToMask[i]] = _maskFill;
            }

            return new string(maskedText);
        }

        Range CalculateRange(int start, int end)
        {
            var range = new Range();

            for (int i = start; i <= end && i < Mask.Length; i++)
            {
                if (_maskToRaw[i] != -1)
                {
                    if (range.Start == -1)
                        range.Start = _maskToRaw[i];

                    range.End = _maskToRaw[i];
                }
            }

            if (end == Mask.Length)
                range.End = _rawText.Length;

            if (range.Start == range.End && start < end)
            {
                var newStart = PreviousValidPosition(range.Start - 1);

                if (newStart < range.Start)
                    range.Start = newStart;
            }

            return range;
        }

        string Clear(string str)
        {
            foreach (var c in _charsInMask)
                str = str.Replace(c.ToString(), "");

            return str;
        }

        void ITextWatcher.BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            if (Mask != null)
            {
                if (!_editingBefore)
                {
                    _editingBefore = true;

                    if (start > _lastValidMaskPosition)
                        _ignore = true;

                    var rangeStart = start;

                    if (after == 0)
                        rangeStart = ErasingStart(start);

                    var range = CalculateRange(rangeStart, start + count);

                    if (range.Start != -1)
                        _rawText.SubtractFromString(range);

                    if (count > 0)
                        _selection = PreviousValidPosition(start);
                }
            }
        }

        void ITextWatcher.OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            if (Mask != null)
            {
                if (!_editingOnChanged && _editingBefore)
                {
                    _editingOnChanged = true;

                    if (_ignore)
                        return;

                    if (count > 0)
                    {
                        var startingPosition = _maskToRaw[NextValidPosition(start)];
                        var addedString = s.SubSequence(start, start + count).ToString();
                        count = _rawText.AddToString(Clear(addedString), startingPosition, _maxRawLength);
                        if (_initialized)
                        {
#pragma warning disable CC0105 // You should use 'var' whenever possible.
                            int currentPosition = startingPosition + count < _rawToMask.Length
                                ? _rawToMask[startingPosition + count]
                                : currentPosition = _lastValidMaskPosition + 1;
#pragma warning restore CC0105 // You should use 'var' whenever possible.

                            _selection = NextValidPosition(currentPosition);
                        }
                    }
                }
            }
        }

        void ITextWatcher.AfterTextChanged(IEditable s)
        {
            if (Mask != null)
            {
                if (!_editingAfter && _editingBefore && _editingOnChanged)
                {
                    _editingAfter = true;

                    if (_rawText.Length == 0 && HasHint)
                    {
                        _selection = 0;
                        Text = null;
                    }
                    else
                        Text = MakeMaskedText();

                    _selectionChanged = false;
                    SetSelection(_selection);

                    _editingBefore = false;
                    _editingOnChanged = false;
                    _editingAfter = false;
                    _ignore = false;
                }
            }
        }

        #endregion
    }

    class Range
    {
        #region Constructors and Destructors

        internal Range()
        {
            Start = -1;
            End = -1;
        }

        #endregion

        #region Public Properties

        public int Start { get; set; }

        public int End { get; set; }

        #endregion
    }

    class RawText
    {
        #region Fields

        string _text = "";

        #endregion

        #region Public Properties

        internal string Text
            => _text;

        internal int Length
            => _text.Length;

        internal char this[int position]
            => _text[position];

        #endregion

        #region Public Methods and Operators

        internal void SubtractFromString(Range range)
        {
            var firstPart = "";
            var lastPart = "";

            if (range.Start > 0 && range.Start <= _text.Length)
                firstPart = _text.Substring(0, range.Start);

            if (range.End >= 0 && range.End < _text.Length)
                lastPart = _text.Substring(_text.Length);

            _text = firstPart + lastPart;
        }

        internal int AddToString(string newString, int start, int maxLength)
        {
            var firstPart = "";
            var lastPart = "";

            if (string.IsNullOrEmpty(newString))
                return 0;

            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start), "Start position must be non-negative");
            if (start > _text.Length)

                throw new ArgumentOutOfRangeException(nameof(start), "Start position must be less than the actual text length");

            var count = newString.Length;

            if (start > 0)
                firstPart = _text.Substring(0, start);

            if (start >= 0 && start < _text.Length)
                lastPart = _text.Substring(start, _text.Length);

            if (_text.Length + newString.Length > maxLength)
            {
                count = maxLength - _text.Length;
                newString = newString.Substring(0, count);
            }

            _text = firstPart + newString + lastPart;

            return count;
        }

        #endregion
    }
}