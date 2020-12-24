Function GetPet([string]$urlDomain)
{
    $pet_id = Read-Host 'Please enter Pet Id'
    $headers = @{
        Authorization = "Bearer $accessToken"
    }

    try
    {
        $response = Invoke-RestMethod -Uri $urlDomain/api/pets/$pet_id -Headers $headers -ContentType 'application/json'
    }
    catch
    {   
        if ( $null -ne $_.Exception.response -and $_.Exception.response.StatusCode -eq 'Unauthorized' )
        {
            Write-Warning 'Token is expired, authorize to generate new one!'
            AuthUser($urlDomain)
            exit
        }
        else 
        {
           throw
        }
    }

    Write-Output $response
}
