# AiPD1
Project made for university course "Analysis and processing of sound".

It's main functionality is reading .wav files and displaying it's chart and parameters in time domain.

## Frontend Angular
* angular-google-charts
* Angular Material

## Backend ASP .net 5.0
* NAudio

## App
App has two screens. Main screen allows to load multiple files from predefined catalog. For each file there is waveform chart and basic parameters computed on backend.

![main screen image](https://github.com/KicunKrzysztof/AiPD1/blob/main/main.png)

Second screen contains detailed information about loaded file. Charts, on main and detailed screen, are interactive. There is also functionality of saving computed parameters to .csv file by clicking "ZAPISZ CSV" button.
![detailed screen image](https://github.com/KicunKrzysztof/AiPD1/blob/main/detail.jpg)
