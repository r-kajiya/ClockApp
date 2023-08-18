# ClockApp

## Overview
Clock App with Windows.

It will also support ios/ipad devices in the feature.

## Feature

### Clock
You can check the User`s time zone time.

### Timer
You can set the timer.

<img src="ReadMe_Resource/RMR_001.PNG" width="50%" />

Can Start, Stop, Reset, Pause.

Play Alert when finished.

If you want to stop the alert. please tap the button.

### Stopwatch
You can also use the stopwatch.

Can Start, Stop, Reset, and lap time.

## DevEnv
#### Windows 10/11
#### Unity2021.3.4f1

## Internal Requirement
### Dev Requirement
#### 1. The code must implement Edit Mode and Play Mode tests, or your leader will not sign off on the project.
It uses the Unity Test Framework to Support "Edit Mode" and "Play Mode" testing.

#### 2. Please create C# interface for next feature to integrate with another work operation application. 
I was created it so that it can be easily replaced with an algorithm preparation using the interface.

It is also supported by loading ClockApp.scene.

#### 3. Use Rx(Reactive Extensions) .
I am using.

#### 4. Implement interface to divide dependency as Di(Dependency Injection) All our C# projects are using Rx and Di. Unity projects use Rx package UniRx and Di package Zenject or VContainer. Please use these packages for this project. 
The ClockApp project uses Rx and Di (Dependency Injection) to implement dependencies divide.

DI package use of VContainer.

### Document Requirement 
#### 1. As we said, this application will be used on iOS/iPad devices. Do you have any concern for UI?
iPhone and iPad have various resolutions and safe areas, so consider and implement.

ClockApp is implemented considering various resolutions and safe areas, so it is easy to handle.

#### 2. How would you refactor the code and/or project after release? What would you prioritize as “must happen” versus “nice to have” changes. 
##### Must happen
・Support CI/CD for build and test.

・Register team members in the repository.

・Receive feedback from and external members and make list.

・Documentation of code, specifications, etc.

##### Nice to have
・only the minimum basic feature, we will improve the functions based on feedback.

・Guaranteed to work in the background for iOS/iPad.

・It`s just my personal preference, I would like to add more game ux feature.

#### 3. [Optional] This application will be used on VR application. Share your concern and your opinion on what need to take into account to support it in VR?
I have never developed a VR application, so i do not if I can give you proper advice, but I think I am concerned about the following.

##### Input system fixes
instead of tapping, we have to develop for gaze control and controller control.

##### UI fixes
I do not think the ClockApp has a UI suitable for VR, so I think I will create it as an eye-tracking UI and make it as unobtrusive as possible.

## Time spent
#### Read assignment document : 30 minute
#### Install unity : 10 minute
#### Create repository : 20 minute
#### Learning VContainer : 2 hour
#### Learning Unity Test Framework : 1 hour
#### Application program design :  5 hour
#### Application implementation : 5 hour
#### Application debug : 30 minute
#### Application fix : 1 hour
#### Write document : 1 hour
#### Wrap up assignment : 30 minute
#### Total : 17 hour