# Ponder
This CLI tool is a simple way to create a markdown file with a table of contents based on the headers in the file. It is a work in progress and is not yet ready for use.

### Dependencies
* .NET 7.0

### Running the App
* Clone the repo
* Build the app with 'dotnet build'
* Run the app with 'dotnet run'
* Optionally, set add the bin directory to your PATH to run the app from anywhere with `ponder <filename>`

### Adding bin to PATH
#### macOS
* Open your shell profile in your home directory with `vim ~/.zshrc`
* Update the Path variable with the path to the bin directory
```bash
export PATH ="home/repos/ponder/Ponder/bin/Debug/net7.0:$PATH"
```
* Save and exit the file with `:wq`
* Reload the shell profile with `source ~/.zshrc` or by launching a new terminal window
* Run the app with `ponder` to verify the path is set correctly


## Requirements
* [ ] The app should be able to read a markdown file and generate a table of contents based on the headers in the file.