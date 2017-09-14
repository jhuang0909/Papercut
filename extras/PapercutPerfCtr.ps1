$categoryName = "Papercut"

# If you need to delete the performance object and have it re-created call this:
[System.Diagnostics.PerformanceCounterCategory]::Delete($categoryName)


$categoryHelp = "A Performance object for Papercut, a stupidly simple SMTP server for local development."
$categoryType = [System.Diagnostics.PerformanceCounterCategoryType]::SingleInstance

$categoryExists = [System.Diagnostics.PerformanceCounterCategory]::Exists($categoryName)


If (-Not $categoryExists)
{
  $objCCDC = New-Object System.Diagnostics.CounterCreationDataCollection
  
  $objCCD1 = New-Object System.Diagnostics.CounterCreationData
  $objCCD1.CounterName = "MessageCount"
  $objCCD1.CounterType = "NumberOfItems32"
  $objCCD1.CounterHelp = "Number of messages received since Papercut starts."
  $objCCDC.Add($objCCD1) | Out-Null
  
  $objCCD2 = New-Object System.Diagnostics.CounterCreationData
  $objCCD2.CounterName = "Received Messages/sec"
  $objCCD2.CounterType = "RateOfCountsPerSecond32"
  $objCCD2.CounterHelp = "Rate of received messages."
  $objCCDC.Add($objCCD2) | Out-Null

  $objCCD3 = New-Object System.Diagnostics.CounterCreationData
  $objCCD3.CounterName = "WebserviceCallCount"
  $objCCD3.CounterType = "NumberOfItems32"
  $objCCD3.CounterHelp = "Number of web service calls received since Papercut starts."
  $objCCDC.Add($objCCD3) | Out-Null

  $objCCD4 = New-Object System.Diagnostics.CounterCreationData
  $objCCD4.CounterName = "Received Webservice Calls/sec"
  $objCCD4.CounterType = "RateOfCountsPerSecond32"
  $objCCD4.CounterHelp = "Rate of received web service calls."
  $objCCDC.Add($objCCD4) | Out-Null

  [System.Diagnostics.PerformanceCounterCategory]::Create($categoryName, $categoryHelp, $categoryType, $objCCDC) | Out-Null
}


Read-Host