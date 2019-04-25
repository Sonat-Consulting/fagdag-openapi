const jwt = require('jsonwebtoken');

const tokenPublicKey = `-----BEGIN PUBLIC KEY-----
MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA2gfve+qvllRVIUxYMp3s
rZDeElr/eyYZ6oG3Bng/AhVlVv4YrmpmB75Dnvm0ONOh6TaRzN9LfqIYAIRwiMR8
x2uHs3jVbUhf3jKBfhfAiTOHf23927Tk4Z/Lj7INbKw6QV2/XdokzO2xcM+N1k81
LLwCPHAT0G8+ABgjjvidXyv5AWAX2FY+fY/pMkbHCDT3NLKwLSR5ngv1Dx+xoWWj
2Vu7BQj/fj5AFH5E8vKv7ElghHRvcOJlBUYgAcs9gb6ad+wLY7ttkoCaZJQAcaMG
ag4QJ/mqWzVsDbuff9/7rrRsJ/7G1Rb2f5h5MTMxvF4Mh+C24ADzGkvOF6/9O8KK
mwIDAQAB
-----END PUBLIC KEY-----
`;

const verifyToken = (permission) => (req, res, next) => {
    let token = req.headers['x-access-token'] || req.headers['authorization']; // Express headers are auto converted to lowercase

    if (token && token.startsWith('Bearer ')) {
        // Remove Bearer from string
        token = token.slice(7, token.length);
    }

    if (!token) {
        return res.status(403).send({ auth: false, message: 'No token provided.' });
    }

    jwt.verify(token, tokenPublicKey, (err, decoded) => {
        if (err) {
            return res.status(500).send({ auth: false, message: 'Failed to authenticate token.' });
        }

        if (permission && !decoded.permissions.includes(permission)) {
            return res.status(403).send({ auth: false, message: 'No valid permission.' });
        }

        // if everything is good, save userId to request for use in other routes
        req.userId = decoded.sub;

        next();
    });
}

module.exports = {
    verifyToken,
    permissions: {
        read: 'read:employees',
        write: 'modify:employees',
    },
};
