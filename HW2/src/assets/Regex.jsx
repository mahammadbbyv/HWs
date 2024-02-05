export const validEmail = new RegExp(
    '^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$'
);

export const validUsername = new RegExp(
    '^[a-zA-Z][a-zA-Z0-9_-]{2,15}$'
);

export const validName = new RegExp(
    '^[a-zA-Z]+(?:[\'-][a-zA-Z]+)*$'
);