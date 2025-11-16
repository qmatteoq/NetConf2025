# Fix UTF-8 encoding for all Razor files
$razorFiles = @(
    "Components\Pages\Home.razor",
    "Components\Pages\Feedback.razor",
    "Components\Layout\NavMenu.razor",
    "Components\Layout\MainLayout.razor",
    "Components\Routes.razor",
    "Components\App.razor",
    "Components\_Imports.razor"
)

foreach ($file in $razorFiles) {
    $fullPath = Join-Path $PSScriptRoot $file
    if (Test-Path $fullPath) {
        Write-Host "Processing: $file"
        $content = Get-Content $fullPath -Raw -Encoding UTF8
        $utf8WithBom = New-Object System.Text.UTF8Encoding $true
        [System.IO.File]::WriteAllText($fullPath, $content, $utf8WithBom)
        Write-Host "  ? Converted to UTF-8 with BOM"
    }
}

Write-Host "`nAll files processed!"
