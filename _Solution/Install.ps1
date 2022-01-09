# PowerShell script to install example Rates windows task into windows task scheduler, runs every 30 minutes
# Remember to set batch processing rights in the local group policy, this can be found at:
#     Administrator Tools: Local Security Policy\Security Settings\Local Policies\User Rights Assignment
# Add the standard account [NT AUTHORITY\SYSTEM] to the "Log on as a batch job" setting

Write-Output "Installing Rates.exe as a windows task..."

$Trigger = New-ScheduledTaskTrigger -Once -At (Get-Date) -RepetitionInterval (New-TimeSpan -Minutes 30)
$User = "NT AUTHORITY\SYSTEM"
$Action = New-ScheduledTaskAction -Execute "C:\Utilities\Rates\Rates.exe"

Register-ScheduledTask -Force -TaskName "FX Rates Collection" -Trigger $Trigger -User $User -Action $Action -RunLevel Highest
