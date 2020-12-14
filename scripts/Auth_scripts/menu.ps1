Function getMenu($url)
{
    $item = Read-Host @'
1: Get pet by id.
2: Unavail
3: Unavail
Please select the task [1-3] 
'@
                switch ($item)
                {
                    default { Write-Warning 'Please choose from given options' }
                    '1' { getPet($url); break }
                    '2' { 'You chose option #2' } 
                    '3' { 'You chose option #3' }
                }
}