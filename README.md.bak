# DDoS Attack Bot

This repository contains a Telegram bot designed to perform DDoS attacks. The bot allows authorized users to initiate attacks against specified targets. It includes various commands for managing users, logging activities, and viewing information about attacks. 

## Features

- **User Management**: Add, remove, and view authorized users.
- **Command Logging**: Log details of each command executed by users.
- **Attack Commands**: Initiate DDoS attacks with specified parameters.
- **Cooldown Management**: Prevent users from running multiple commands in quick succession.
- **Admin Commands**: Special commands for admins to manage the bot and its users.
- **Broadcast Messages**: Admins can send messages to all authorized users.

## Prerequisites
-github codespaces ( 4 core)
- Python 3.x
- `telebot` library
- Telegram Bot Token (You Can Use Botfather)

## Getting Started

### Installation

1. Clone the repository:

   ```sh
   git clone https://github.com/yourusername/ddos-attack-bot.git
   cd ddos-attack-bot
   ```

2. Install the required libraries:

   ```sh
   pip install telebot
   pip install flask
   pip install aiogram
   ```

### Configuration

1. Create a bot on Telegram and obtain your bot token.

2. Replace the placeholder token in the script with your actual bot token:

   ```python
   bot = telebot.TeleBot('YOUR_BOT_TOKEN')
   ```

3. Add your admin user IDs in the `admin_id` list:

   ```python
   admin_id = ["YOUR_ADMIN_ID"]
   ```

### Usage

1. Run the bot:

   ```sh
   chmod +x *
   python watcher.py
   ```

2. Interact with the bot on Telegram using the available commands.

## Bot Commands

### User Commands

- `/start` - Welcome message.
- `/help` - Display help information and available commands.
- `/bgmi <target> <port> <time>` - Initiate a DDoS attack on the specified target.
- `/rules` - Display rules for using the bot.
- `/plan` - Display available plans and pricing.
- `/mylogs` - Show recent command logs for the user.
- `/myinfo` - Display user's information and approval status.

### Admin Commands

- `/add <userId> <duration>` - Add a user with a specified approval duration.
- `/remove <userId>` - Remove a user from the authorized list.
- `/allusers` - Display all authorized users.
- `/logs` - Display logs of all commands executed by users.
- `/clearlogs` - Clear the command logs.
- `/clearusers` - Clear the list of authorized users.
- `/broadcast <message>` - Send a broadcast message to all authorized users.

## File Structure

- `ddos_bot.py` - Main bot script containing all functionalities.
- `users.txt` - File to store allowed user IDs.
- `log.txt` - File to store command logs.
- `keep_alive.py` - Script to keep the bot running (e.g., for use with Repl.it).

## Logging

The bot logs details of each command executed by users, including user ID, command, target, port, and time. Logs are stored in the `log.txt` file.

## User Management

The bot supports adding and removing authorized users, as well as setting approval expiry dates for each user. Admins can view the list of all authorized users and clear the user list if needed.

## License

This project is licensed under the MIT License.

## Disclaimer

This bot is intended for educational purposes only. The misuse of this bot may result in legal consequences. The author is not responsible for any misuse of this bot.

---

**Note**: Replace `YOUR_BOT_TOKEN` and `YOUR_ADMIN_ID` with your actual bot token and admin user ID before running the bot. Ensure that you comply with all legal regulations and use this bot responsibly.

## Contributing

If you wish to contribute to this project, feel free to submit a pull request or open an issue on GitHub.

## Contact

For any queries or support, contact [venomXcrazy](https://t.me/venomXcrazy) on Telegram. 

---

Happy DDoS'ing responsibly! 🚀
