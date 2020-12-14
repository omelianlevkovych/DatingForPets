Function getPet($url)
{
    $pet_id = Read-Host 'Please enter Pet Id'
    $headers = @{
        Authorization = "Bearer $env:TOKEN"
    }
    try
    {
        $response = Invoke-RestMethod -Uri $url/api/pets/$pet_id -Headers $headers -ContentType "application/json"
    }
    catch
    {  
        if (  $_.Exception.response.StatusCode -eq "Unauthorized" )
        {
            Write-Warning 'Token is expired, authorize to generate new one!'
            authUser($url)
            exit
        }
        else 
        {
           throw
        }
    }  
    Write-Output $response
}
