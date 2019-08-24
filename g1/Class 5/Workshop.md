# Phonebook

Develop a phonebook application that will allow users to save, edit and delete contacts and relevant info

### What we need?

1. User model
    - Name
    - LastName
    - Username
    - Password

2. Contact model
    - Name
    - LastName
    - Phone
    - Email
    - Address
    - UserId (FK)

3. Create a database (Phonebook) with tables corresponding to the above models

4. Create a Web API application that will connect to database and expose endpoints for all relevant actions that we want to perform
(read, create, edit, delete contacts)
    4.1. For the first part it is enough to have one or two users and work mostly with exposing contacts actions (no authorization or login forms needed)