param (
    [string]$server = "https://localhost:5001"
)

# Ignoring self-signed certificates
add-type @"
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    public class TrustAllCertsPolicy : ICertificatePolicy {
        public bool CheckValidationResult(
            ServicePoint srvPoint, X509Certificate certificate,
            WebRequest request, int certificateProblem) {
            return true;
        }
    }
"@
[System.Net.ServicePointManager]::CertificatePolicy = New-Object TrustAllCertsPolicy

$username = Read-Host 'Please enter your username'
$password = Read-Host 'Please enter your password' -AsSecureString
$body = @{
    Username=$username
    Password=[Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))
} | ConvertTo-Json
$headers = @{
    "Content-Type" = "application/json"
}
try
{
    $response = Invoke-WebRequest -Uri $server/api/account/login -Method POST -Body $body -Headers $headers
}
catch
{
    if (  $_.Exception.response.StatusCode -eq "Unauthorized" )
    {
        Write-Output "Incorrect username or password"
        .\auth.ps1 -server $server
        exit
    }
    else 
    {
       throw
    }
}
$token = (($response.content | ConvertFrom-Json).token).ToString()
$env:TOKEN = $token