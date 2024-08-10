// Decompiled with JetBrains decompiler
// Type: KeyAuth.Program
// Assembly: Azure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34575351-D411-4450-A7EC-E5B2579DA4D6
// Assembly location: C:\Users\xifib\OneDrive\Bureau\Azure.exe

using System;
using System.Windows.Forms;

namespace KeyAuth
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new Login());
    }
  }
}
