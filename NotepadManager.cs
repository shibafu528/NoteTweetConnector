using System;
using System.Text;
using System.Runtime.InteropServices;

namespace NoteTweetConnector
{
	public class NotepadManager
	{
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wp, byte[] lp);

		[DllImport("user32.dll")]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		private static extern IntPtr FindWindowEx(IntPtr hWnd, IntPtr hwndChildAfter, string lpszChass, string lpszWindow);

		[DllImport("user32.dll")]
		private static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

		public const uint WM_SETTEXT = 0x000C;
		public const int GWL_HINSTANCE = (-6);

		private IntPtr hInstance;
		private IntPtr hWndNotepad;
		private IntPtr hWndEdit;

		public NotepadManager()
		{
			hWndNotepad = FindWindow("NotePad", null);
			if (hWndNotepad != IntPtr.Zero)
			{
				hWndEdit = FindWindowEx(hWndNotepad, IntPtr.Zero, "Edit", null);
				hInstance = GetWindowLong(hWndNotepad, GWL_HINSTANCE);
			}
			else
				throw new InvalidOperationException();
		}

		public void setText(string text)
		{
			SendMessage(hWndEdit, WM_SETTEXT, IntPtr.Zero, Encoding.GetEncoding(932).GetBytes(text));
		}

		public void hookWindow()
		{
		}

		public void unHookWindow()
		{
		}
	}
}

