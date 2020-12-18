Function AuthUser([string]$urlDomain)
{
    while($true)
    {
        $username = Read-Host 'Please enter your username'
        $password = Read-Host 'Please enter your password' -AsSecureString
        $body = @{
            Username = $username
            Password = [Runtime.InteropServices.Marshal]::PtrToStringAuto([Runtime.InteropServices.Marshal]::SecureStringToBSTR($password))
        } | ConvertTo-Json
        $headers = @{
            'Content-Type' = 'application/json'
        }
        try
        {
            $response = Invoke-WebRequest -Uri $urlDomain/api/account/login -Method POST -Body $body -Headers $headers
        }
        catch
        {   
            if ( $null -ne $_.Exception.response -and $_.Exception.response.StatusCode -eq 'Unauthorized' )
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
    Set-Variable -Name 'accessToken' -Scope global -Value (($response.content | ConvertFrom-Json).token).ToString()
    # Call previous function
    &(Get-PSCallStack | Select-Object -Property *)[1].FunctionName $urlDomain
}