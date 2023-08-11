# Use an official SQLite image as the base image
FROM alpine:3.14

# Install SQLite package
RUN apk add --no-cache sqlite

# Create a directory for the SQLite database
RUN mkdir /data

# Copy the SQLite database file into the container
COPY mydatabase.db /data/mydatabase.db

# Set the working directory
WORKDIR /data

# Expose the SQLite database port
EXPOSE 5432

# Command to start SQLite shell with the database
CMD ["sqlite3", "mydatabase.db"]
