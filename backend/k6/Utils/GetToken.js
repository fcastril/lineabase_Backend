import http from 'k6/http';
import { fail } from 'k6';
import { BASE_URL_DEV, variables } from './Variables.js'

const headers = {
    'Content-Type': 'application/json'
};

export default function getToken() {
    const accessCredentialsBody = {
        "userName": variables.credentials.username,
        "password": variables.credentials.password
    };

    let loginToGetBearerToken = http.post(BASE_URL_DEV + variables.endPoint.securityLogin , JSON.stringify(essCredentialsBody),
        { headers: headers, timeout: '5m30s' });
    if (loginToGetBearerToken.status === 200) {
        const responseRequest = JSON.parse(loginToGetBearerToken.body)
        const bearerToken = responseRequest.data.token
        return bearerToken
    } else {
        const responseErrorGetToken = JSON.parse(loginToGetBearerToken.body)
        fail('Unexpected response get token: ' + loginToGetBearerToken.status + " " + responseErrorGetToken.Message)
    }
}