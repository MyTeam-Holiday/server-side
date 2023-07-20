param (
    [string]
    [Parameter(Mandatory=$true)]
    $PathSource,

    [string]
    [Parameter(Mandatory=$true)]
    $PathTarget,

    [string]
    $EnvName
)
# Check if the AppVeyor module is installed
#$modules = Get-Module -ListAvailable
#if ($modules.Name -contains "AppVeyor") {
#    Write-Host "The AppVeyor module is installed."
#} else {
#    Write-Host "The AppVeyor module is not installed. Will install now."
    # Install the AppVeyor module
#    Install-Module AppVeyor
#}


# Get the value of the secure string from AppVeyor or the local environment
#Временно только локал (перед использованием один раз запустить setenv.ps1 с правильными значениями)
$local = 1

#$scriptPath = $MyInvocation.MyCommand.Path

$appSettingsPathSource = "$PathSource\appsettings.$EnvName.json"
$appSettingsPathTarget = "$PathTarget\appsettings.json"

if ($local) {
    $server_ip = $env:server_ip
    $db_port = $env:db_port
    $db_user_id = $env:db_user_id
    $db_password = $env:db_password
    $db_name = $env:db_name
    $client_id = $env:client_id
    $client_secret = $env:client_secret
    $client_domain = $env:client_domain
    $email_host = $env:email_host
    $email_address = $env:email_address
    $email_password = $env:email_password
    $http_port = $env:http_port
    $https_port = $env:https_port
} else {
    #$server_ip = Get-AppVeyorVariable "server_ip"
    #$db_port = Get-AppVeyorVariable "db_port"
    #$db_user_id = Get-AppVeyorVariable "db_user_id.dev"
    #$db_password = Get-AppVeyorVariable "db_password.dev"
    #$db_name = Get-AppVeyorVariable "db_name.dev"
    #$client_id = Get-AppVeyorVariable "client_id.dev"
    #$client_secret = Get-AppVeyorVariable "client_secret.dev"
    #$client_domain = Get-AppVeyorVariable "client_domain"
    #$email_host = Get-AppVeyorVariable "email_host"
    #$email_address = Get-AppVeyorVariable "email_address"
    #$email_password = Get-AppVeyorVariable "email_password"
    #$http_port = Get-AppVeyorVariable "http_port"
    #$https_port = Get-AppVeyorVariable "https_port"
}

# Define the dictionary to store placeholders and their corresponding secure variables or environment variables
$placeholderDictionary = @{
    "{server_ip}" = $server_ip
    "{db_port}" = $db_port
    "{db_user_id}" = $db_user_id
    "{db_password}" = $db_password
    "{db_name}" = $db_name
    "{client_id}" = $client_id
    "{client_secret}" = $client_secret
    "{client_domain}" = $client_domain
    "{email_host}" = $email_host
    "{email_address}" = $email_address
    "{email_password}" = $email_password
    "{http_port}" = $http_port
    "{https_port}" = $https_port
}

# Load the JSON content from the appsettings.json file
$jsonContent = Get-Content $appSettingsPathSource

# Iterate through the placeholders and replace them with the appropriate values
foreach ($key in $placeholderDictionary.Keys) {
  Write-Host key: $key, value: $placeholderDictionary[$key]
  $jsonContent = $jsonContent -replace "$key", $placeholderDictionary[$key]
}

# Convert the updated JSON object back to a string and write it to the appsettings.json file
Set-Content $appSettingsPathTarget $jsonContent