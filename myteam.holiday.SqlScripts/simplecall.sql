CALL th_PreCreateUser('UserName', 'UserEmail', @a);
SELECT @a;

CALL th_CreateUser(@a, 'Passhash', 'Passsalt', @b);
SELECT @b;