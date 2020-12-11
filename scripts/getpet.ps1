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

$pet_id = Read-Host 'Please enter Pet Id'

$headers = @{
    Authorization = "Bearer $env:TOKEN"
}

try
{
    $response = Invoke-RestMethod -Uri $server/api/pets/$pet_id -Headers $headers -ContentType "application/json"
}
catch
{  
    if (  $_.Exception.response.StatusCode -eq "Unauthorized" )
    {
        Write-Output "Token is expired, authorize to generate new one!"
        .\auth.ps1 -server $server
        .\getpet.ps1 -server $server
        exit
    }
    else 
    {
       throw
    }
}

Write-Output $response