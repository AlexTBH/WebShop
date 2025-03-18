$apiBaseUrl = "https://localhost:7011"
$body = @{email = "myemail@email.se"; password = "ABCdef123!?_"; } | ConvertTo-Json
$registerRawResponse = Invoke-WebRequest -uri "$apiBaseUrl/Account/register" -method POST -body $body -ContentType "application/json"
$registerRawResponse
$loginResponse = Invoke-Restmethod -uri "$apiBaseUrl/Account/login?useCookies=true" -method POST -body $body -ContentType "application/json" -SessionVariable session
$loginResponse
$cookie = $session.Cookies.GetCookies($apiBaseUrl)
$cookie
$authenticatedResponse = Invoke-RestMethod -uri "$apiBaseUrl/Account/AuthenticatedUser" -method GET -ContentType "application/json" -WebSession $session
$authenticatedResponse
