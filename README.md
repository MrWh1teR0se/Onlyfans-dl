# OnlyFansDL (.NET Core)

---

This tool downloads all photos/videos from an OnlyFans profile, creating a local archive. 

You must be subscribed to the profile to download their content.
OnlyFansDL (.NET Core) creates a directory in the repo directory that corresponds to the profile you download. Each profile you download is created as a root directory. Within each profile directory, all media is downloaded in photos/ and videos/.


### Built With

* [.NET Core](https://github.com/dotnet/core)
* [RestSharp](https://restsharp.dev/)
* [Rider](https://www.jetbrains.com/rider/)

## Disclaimer
<p style="color:red">
This repository is for research purposes only, the use of this code is your responsibility.
I take NO responsibility and/or liability for how you choose to use any of the source code available here. By using any of the files available in this repository, you understand that you are AGREEING TO USE AT YOUR OWN RISK. Once again, ALL files available here are for EDUCATION and/or RESEARCH purposes ONLY.
</p>


## Getting Started

To get a local copy up and running follow these simple steps.

### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/MrWh1teR0se/OnlyFansDL.git
   ```
2. Install NuGet packages
   ```sh
   dotnet restore
   ```

## Usage

1. start the Program (Debug/Release)
2. Go to [OnlyFans](https://onlyfans.com) and login with your Credentials

    2.1 copy your AuthToken (open the Developer-Console > Console )
   ```js
   copy(localStorage.getItem("accessToken"))
   OR
   localStorage.getItem("accessToken")
   ```
3. enter your AccessToken and press [ENTER]
   
    3.1 if the authentication was successful the username and the UserID appears as green output text:
   <span style="color:green">USERNAME / USERID</span>
   
4. enter the username of the OnlyFans model/actor whose content you wish to download.
5. wait and let the magic begins

## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.