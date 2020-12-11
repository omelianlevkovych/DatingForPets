param (
    [string]$server = "https://localhost:5001"
)
$item = Read-Host @"
1: Get pet by id.
2: Unavailable
3: Unavailable
Please select the task [1-3] 
"@
switch ($item)
{
    '1'{
    .\getpet.ps1 -server $server
    exit
    } 
    '2'{
    'You chose option #2'
    } 
    '3'{
    'You chose option #3'
    }
}