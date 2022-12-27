
Remove-Item Alias:curl -ErrorAction Ignore

$random =  New-Object System.Random
while($true) {
    for ($i=0; $i -lt $random.Next(50); $i++) {
        curl -s http://54.82.8.83:8080 | Out-Null
    }
    curl -s http://54.82.8.83:8080?slow | Out-Null
    for ($i=0; $i -lt $random.Next(30); $i++) {
        curl -s http://54.82.8.83:8081 | Out-Null
    }
    curl -s http://54.82.8.83:8081?slow | Out-Null
}
