namespace BadStickyNotes;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        NewNoteButton = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // NewNoteButton
        // 
        NewNoteButton.Location = new System.Drawing.Point(12, 12);
        NewNoteButton.Name = "NewNoteButton";
        NewNoteButton.Size = new System.Drawing.Size(321, 63);
        NewNoteButton.TabIndex = 0;
        NewNoteButton.Text = "New Note";
        NewNoteButton.UseVisualStyleBackColor = true;
        NewNoteButton.Click += NewNoteButton_Click;
        NewNoteButton.MouseEnter += NewNoteButton_MouseEnter;
        NewNoteButton.MouseLeave += NewNoteButton_MouseLeave;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(345, 450);
        Controls.Add(NewNoteButton);
        Text = "Bad Sticky Notes";
        Click += Form1_Click;
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button NewNoteButton;

    #endregion
}