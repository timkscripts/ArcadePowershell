
import-Module "C:\Windows\System32\WindowsPowerShell\v1.0\Modules\WASP\wasp.dll"
#$processName = "snes9x";
#$location = "F:\arcade\snes\Roms\shortcuts\Aladdin.lnk";
$location = Resolve-Path "F:\arcade\snes\Roms\Roms\Aladdin.smc"

$script:nativeMethods = @();
function Register-NativeMethod([string]$dll, [string]$methodSignature)
{
    $script:nativeMethods += [PSCustomObject]@{ Dll = $dll; Signature = $methodSignature; }
}
function Add-NativeMethods()
{
    $nativeMethodsCode = $script:nativeMethods | % { "
        [DllImport(`"$($_.Dll)`")]
        public static extern $($_.Signature);
    " }

    Add-Type @"
        using System;
        using System.Runtime.InteropServices;
        public class NativeMethods {
            $nativeMethodsCode
        }
"@
}
###########################################################################################


# Add your Win32 API here:
###########################################################################################
Register-NativeMethod "user32.dll" "bool SetForegroundWindow(IntPtr hWnd)"
Register-NativeMethod "user32.dll" "bool ShowWindow(IntPtr hWnd, int nCmdShow)"
Register-NativeMethod "user32.dll" "bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint)"
Register-NativeMethod "user32.dll" "bool AnimateWindow(IntPtr hwnd, UInt32 dwTime, UInt32 dwFlags)"
Register-NativeMethod "user32.dll" "IntPtr FindWindow(IntPtr ZeroOnly, string lpWindowName)"
Register-NativeMethod "user32.dll" "void keybd_event(Byte bVk, Byte bScan, UInt32 dwFlags, UInt32 dwExtraInfo)"
Register-NativeMethod "user32.dll" "bool InvalidateRect(IntPtr hWnd, IntPtr ZeroOnly, bool bErase)"
Register-NativeMethod "user32.dll" "int GetMenuItemCount(IntPtr hMenu)"
Register-NativeMethod "user32.dll" "IntPtr GetMenu(IntPtr hWnd)"
Register-NativeMethod "user32.dll" "IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName)"
Register-NativeMethod "user32.dll" "bool DrawMenuBar(IntPtr hWnd)"
Register-NativeMethod "user32.dll" "bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags)"


Register-NativeMethod "user32.dll" "int GetWindowLong(IntPtr hWnd, int nIndex)"
Register-NativeMethod "user32.dll" "int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong)"
Register-NativeMethod "user32.dll" "bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags)"

###########################################################################################


#Build class and registers them:
Add-NativeMethods

# Start notepad.exe and gets it's MainWindowTitle
$ProcessActive = Get-Process $processName -ErrorAction SilentlyContinue; 
if ($ProcessActive -eq $null){
#Invoke-Item $location
start-process "F:\arcade\snes\Emu\snes9x.exe" -argumentlist "/r $($location.path)"
} else {stop-process -n $processName; Invoke-Item $location};sleep -s 1



$ProcessTitle = Get-Process $processName |?{$_.mainWindowTitle} |select -ExpandProperty MainWindowTitle
$hWnd=[NativeMethods]::FindWindow(0, "$ProcessTitle")




# Real working examples demonstrating how to use Win32 API's from this script:
###########################################################################################
    $WS_BORDER = [int]8388608;
    $WS_DLGFRAME = [int]4194304;
    $WS_CAPTION = $WS_BORDER -bor $WS_DLGFRAME;
    $WS_SYSMENU = [int]524288;
    $WS_THICKFRAME = [int]262144;
    $WS_MINIMIZE = [int]536870912;
    $WS_MAXIMIZEBOX = [int]65536;
    $GWL_STYLE = [int]-16L;
    $GWL_EXSTYLE =[int] -20L;
    $WS_EX_DLGMODALFRAME = [int]0x1L;
    $SWP_NOMOVE = [int]0x2;
    $SWP_NOSIZE = [uint32](0x1);
    $SWP_FRAMECHANGED = [uint32](0x20);
    $WS_VISIBLE = [uInt32]0x10000000;

$MF_BYPOSITION =  [uint32](0x400);
$MF_REMOVE = [uint32](0x1000);
                $process = ((Get-Process -name $processName).MainWindowHandle);


$hmenu = [NativeMethods]::GetMenu($process);

                $count = [NativeMethods]::GetMenuItemCount($hmenu);

                for ($i = 0; $i -lt $count; $i++){
                    [NativeMethods]::RemoveMenu($hmenu, 0, ($MF_BYPOSITION));
                    }


                [NativeMethods]::DrawMenuBar($process);

[NativeMethods]::SetWindowLong($process, $GWL_STYLE, ($WS_VISIBLE));


# InvalidateRect - Redraws window.  Needed after AnimateWindow AW_ACTIVATE
[NativeMethods]::InvalidateRect($hWnd, 0, "true") | Out-Null
sleep -s 1




[void] [System.Reflection.Assembly]::LoadWithPartialName("System.Drawing")
[void] [System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms")