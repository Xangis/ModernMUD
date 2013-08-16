using System;

namespace ModernMUDEditor
{
    public class CheckBoxArray : System.Collections.CollectionBase
    {
        private readonly System.Windows.Forms.Form _hostForm;
        private int _value;
        private System.Windows.Forms.Label _label;

        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public CheckBoxArray(System.Windows.Forms.Form host)
        {
            _hostForm = host;
        }

        public System.Windows.Forms.CheckBox AddNewCheckBox()
        {
            // Create a new instance of the Button class.
            System.Windows.Forms.CheckBox aCheckBox = new System.Windows.Forms.CheckBox();
            // Add the button to the collection's internal list.
            List.Add(aCheckBox);
            // Add the button to the controls collection of the form 
            // referenced by the _hostForm field.
            _hostForm.Controls.Add(aCheckBox);
            // Set intial properties for the button object.
            aCheckBox.Top = ((Count - 1) % 8) * 25 + 10;
            aCheckBox.Left = ((Count - 1) / 8) * 110 + 10;
            aCheckBox.Tag = Count - 1;
            aCheckBox.Text = "(unused)";
            aCheckBox.Click += ClickHandler;
            return aCheckBox;
        }

        public System.Windows.Forms.Label AddValueLabel()
        {
            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            _hostForm.Controls.Add(label);
            label.Top = (Count / 4) * 25 + 10;
            label.Left = 10;
            label.Text = "Value: " + Value;
            _label = label;
            return label;
        }

        public System.Windows.Forms.CheckBox this[int index]
        {
            get
            {
                return (System.Windows.Forms.CheckBox)List[index];
            }
        }

        public void Remove()
        {
            // Check to be sure there is a checkbox to remove.
            if (Count > 0)
            {
                // Remove the last checkbox added to the array from the host form 
                // controls collection. Note the use of the indexer in accessing 
                // the array.
                _hostForm.Controls.Remove(this[Count - 1]);
                List.RemoveAt(Count - 1);
            }
        }

        public void ClickHandler(Object sender, EventArgs e)
        {
            int newvalue = 0;
            for (int count = 0; count < Count; count++)
            {
                System.Windows.Forms.CheckBox ibox = this[count];
                if (ibox.Checked)
                {
                    newvalue += 1 << count;
                }
            }
            _value = newvalue;
            if (_label != null)
            {
                _label.Text = "Value: " + _value;
            }
        }
    }
}
