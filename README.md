# checkers

Builds:

Mono:[![Build Status](https://travis-ci.org/cs451-ducks/checkers.svg?branch=master)](https://travis-ci.org/cs451-ducks/checkers)

Dotnet Core: [![Build Status](https://travis-ci.org/cs451-ducks/checkers.svg?branch=master)](https://travis-ci.org/cs451-ducks/checkers) 


# Steps to run:
```
pre-reqs:
npm, dotnet core 2.1
```
```
# Go to where you unzipped the package
cd checkers-0.0.1-rc
# go into the Checkers directory
cd Checkers/
# build the project
dotnet build
# Run the unit tests
dotnet test Checkers.Test/
# go into the Checkers module
cd Checkers
# Install Typescript Dependencies
npm install
# Compile Typescript to Javascript with Gulp
./node_modules/gulp/bin/gulp.js 
# Run the application, requires sudo to work on port 443
sudo dotnet run
```
