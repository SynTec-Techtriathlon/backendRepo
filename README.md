# Project Name

This repository contains two main components: a Flutter-based mobile application (front-end) and an ASP.NET Core API (back-end). The front-end is located in the `frontend` folder, and the back-end is located in the `backend` folder.


## Prerequisites

Before you begin, ensure you have the following software installed:

- [Flutter](https://flutter.dev/docs/get-started/install)
- [.NET SDK](https://dotnet.microsoft.com/download)

## Setup Instructions

### Setting up the Backend (ASP.NET Core API)

1. Navigate to the `backend` directory by running `cd backend`.

2. Restore NuGet packages using `dotnet restore`.

3. Update the `appsettings.json` file with the necessary configuration settings, such as the database connection string.

4. If database migrations are needed, apply them by running `dotnet ef database update`.

### Setting up the Frontend (Flutter App)

1. Navigate to the `frontend` directory by running `cd frontend`.

2. Install Flutter dependencies with `flutter pub get`.

3. Update the API endpoint URL in the Flutter project, typically found in a configuration file like `lib/config.dart`.

## Running the Project

### Running the Backend

1. In the `backend` directory, start the ASP.NET Core API with `dotnet run`.

   The API will be available at `https://localhost:5001` or `http://localhost:5000` by default.

### Running the Frontend

1. In the `frontend` directory, run the Flutter application using `flutter run`.

   This will launch the app on a connected device or emulator.

## Environment Variables

For both the front-end and back-end, you may need to configure environment variables:

- **Backend:**
  - Update the `appsettings.json` or use environment variables for settings like the database connection string.

- **Frontend:**
  - Update configuration files with the appropriate API endpoint URL.

## Contributing

Contributions are welcome! Please fork this repository, make your changes, and submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

