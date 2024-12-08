#!/bin/sh

# Name of the tmux session
SESSION_NAME="iot"

# Directory containing the start.csproj file
TARGET_DIR="/home/roy/repo/ams/sanddata.no.ams.apps/MqttReader.Console"

# Check if the session already exists
tmux has-session -t $SESSION_NAME 2>/dev/null

# If the session does not exist, create it and start the .NET project
if [ $? != 0 ]; then
    # Create a new session and start it detached
    tmux new-session -d -s $SESSION_NAME

    # Send the command to change directory and run the project
    tmux send-keys -t $SESSION_NAME "cd $TARGET_DIR; dotnet run MqttReader.Con
sole -c Release" C-m
fi
