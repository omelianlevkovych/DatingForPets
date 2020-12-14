Function authUser($url)
{
    while($true)
    {
        $username = Read-Host 'Please enter your username'
        $password = Read-Host 'Please enter your password' -AsSecureString
        $body = @{
            Username=$username
            Password=[Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))
        } | ConvertTo-Json
        $headers = @{
            'Content-Type' = 'application/json'
        }
        try
        {
            $response = Invoke-WebRequest -Uri $url/api/account/login -Method POST -Body $body -Headers $headers
        }
        catch
        {
            if (  $_.Exception.response.StatusCode -eq "Unauthorized" )
            {
                Write-Warning 'Incorrect username or password'
                continue
            }
            else 
            {
               throw
            }
        }
        break
    }
    $token = (($response.content | ConvertFrom-Json).token).ToString()
    $env:TOKEN = $token
    # Call previous function
    &(Get-PSCallStack | Select-Object -Property *)[1].FunctionName $url
}