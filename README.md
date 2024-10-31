#Avalonia Custom Theme Loader for The inbuilt Fluent Theme 
An Avalonia sample application for loading custom themes based on the inbuilt Fluent theme.

I found changing the inbuilt Fluent theme for Avalonia a little tricky to adjust and apply, so I created a small project to learn and to help others. As I don't have too many public repos, my aim for this project is to provide some best practices and a complete, useful small project.

I had to create a workaround and use a bit of code-behind because directly applying a new theme would eventually cause this error:
![image](https://github.com/user-attachments/assets/c1301cb8-abdb-4053-8a63-913ac6494b0e)


## This project features/uses:

Avalonia 11
ReactiveUI
Dependency Injection (via Splat and defined in my Bootstrapper.cs file)
Loading custom themes based on the Fluent Theme Editor (Fluent Theme Editor) via JSON files (from Newtonsoft.Json)
Saving/Loading the chosen theme on startup via reading AppSettings.json

## TODO:

[ ] Save on Exit
[ ] Add Avalonia Tests
[ ] Update UI
[ ] Refactor and clean up to a professional standard
[ ] Provide Summaries for Functions

## Installation & Usage

Downloading the project and running it in Visual Studio 2019 or later should be fine as long as you have the latest version of Avalonia installed. Make sure you edit the Themes.json path in the FileIOService to a folder that your computer can read! I'll implement (work out how to) read the Themes.json file as an embedded resource.

![image](https://github.com/user-attachments/assets/6bc854d5-3ed4-4dff-b3a7-e72f031506a0)



