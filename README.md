## This is my dissertation project for the Westminster International University
# ARMarket
This is mobile application which includes Augmented Reality and shopping features. With this application person can easily choose product which he likes and view this product in AR or buy it.

# Technology that were used:
ARFoundation, Firebase Database and Authentication, Shaders, Unity, C#, responsive UI (supports mobile and tablets), 3D bundles. All application is client-based which means that it receiving data from Firebase Database.

# How it works
Firstly, it's getting data from json file when user going to one of the category. After getting the data it's showing furniture name, price, image them when user choosing one of the option it goes to the Description screen where user can buy furniture or view in AR. If user will click to the view in AR then 3D model of the furniture will be downloaded to the cache of the phone in order to easily access this item in the future. After that ARFoundation functionality will start and user can view his furniture. For the furniture itself i added custom shader that I created myself in order to highlight corners of 3D model. Moreover, if user will create new account then all data would be passed to the Firebase Database that stores user account data (name, phone, address).

# Preview Video
![Alt Text](https://github.com/Omadzze/Example/blob/main/armarketfinal-video.gif)

# Preview Screenshots
![Alt Text](https://firebasestorage.googleapis.com/v0/b/armarket-b2c66.appspot.com/o/arMarketScreenshot1.png?alt=media&token=6e1f7715-4058-453e-a60a-d0c421ebac95)
![Alt Text](https://firebasestorage.googleapis.com/v0/b/armarket-b2c66.appspot.com/o/arMarketScreenshot2.png?alt=media&token=7a985913-9cd8-428f-882e-3f78bd65882d)
![Alt Text](https://firebasestorage.googleapis.com/v0/b/armarket-b2c66.appspot.com/o/arMarketScreenshot3.png?alt=media&token=6010e06a-0cc3-4d99-9f6a-7788a5f757ed)

Full project located on my private repository
