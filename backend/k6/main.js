import http from 'k6/http';
import { check, fail, sleep } from 'k6';
import { describe } from 'https://jslib.k6.io/k6chaijs/4.3.4.3/index.js';
import { BASE_URL_DEV, variables } from './Utils/Variables.js'
import getBearerToken from './Utils/GetToken.js'

export default function () {
    const headers = {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + getBearerToken()
    };

    let idCode = ""
    const idCharacters = "0123456789abcdef"
    for (let i = 0; i < 24; i++) {
        const randomIndex = Math.floor(Math.random() * idCharacters.length)
        idCode += idCharacters.charAt(randomIndex)
    }

    let code = ""
    const charactersCode = "ABCDEFGHIJKLMNOPQRSTUVXYZ"
    for (let i = 0; i < 1; i++) {
        const randomIndex = Math.floor(Math.random() * charactersCode.length)
        code += "-" + charactersCode.charAt(randomIndex)
    }

    let getLastItem = http.get(BASE_URL_DEV + variables.endPoint.getTest, { headers: headers })
    const response = JSON.parse(getLastItem.body)
    const lastItem = response.data.length - 1
    let nameLastItemPlusOne = response.data[lastItem].name + 1

    const bodyForTest = {
        "id": idCode,
        "code": code,
        "name": nameLastItemPlusOne
    }

    const idItem = ["65d39b168652142d8deaec08", "65d39b168652142d8deaec09", "65d39b168652142d8deaec0c", "65d39b168652142d8deaec11", "65d39b168652142d8deaec1b"]
    const codeItem = ["-B", "-C", "-F", "-H", "-R"]
    const nameItem = [4, 5, 8, 13, 23]
    let x = Math.floor(Math.random() * idItem.length)

    describe("Scenario 1: Get all the Test items", () => {
        let getAllTestitems = http.get(BASE_URL_DEV + variables.endPoint.getTest, { headers: headers })
        if (!(getAllTestitems.status === 200)) {
            fail('Unexpected response: ' + getAllTestitems.status);
        } else {
            check(getAllTestitems, {
                'Status Response OK': (response) => response.status === 200,
                'Body status response': (response) => response.json().status === true,
                'Success operation': (response) => response.json().message.includes('successfully'),
            });
        }
    })

    describe("Scenario 2: Successful creating of a new Test item", () => {
        let postCreateTestItem = http.post(BASE_URL_DEV + variables.endPoint.createG, JSON.stringify(bodyForTest),
            { headers: headers })
        if (!(postCreateTestItem.status === 200)) {
            fail('Unexpected response: ' + postCreateTestItem.status);
        } else {
            check(postCreateTestItem, {
                'Status Response OK': (response) => response.status === 200,
                'Body status response': (response) => response.json().status === true,
                'Success operation': (response) => response.json().message.includes('successfully'),
                'Validate the new Test item Id': (response) => response.json().data.id === idCode,
                'Validate the new Test item Code': (response) => response.json().data.code === code,
                'Validate the new Test item Name': (response) => response.json().data.name  === nameLastItemPlusOne,
            });

            let validateCreationTestItem = http.get(BASE_URL_DEV + variables.endPoint.getTest, { headers: headers })
            let x = JSON.parse(validateCreationTestItem.body)
            let lastItem = x.data.length - 1

            if (!(validateCreationTestItem.status === 200)) {
                fail('Unexpected response: ' + validateCreationTestItem.status);
            } else {
                check(validateCreationTestItem, {
                    'New Test item created successfully': (response) => response.json().data[lastItem].id === idCode &&
                        response.json().data[lastItem].code === code && response.json().data[lastItem].name === nameLastItemPlusOne,
                })
            }
        }
    })

    describe("Scenario 3: Successful editing of a Testitem", () => {
        bodyForTest.code += " Edited"
        bodyForTest.name += 1

        let putEditTestItem = http.put(BASE_URL_DEV + variables.endPoint.editTest, JSON.stringify(bodyForTest),
            { headers: headers })
        if (!(putEditTestItem.status === 200)) {
            fail('Unexpected response: ' + putEditTestItem.status + putEditTestItem.body);
        } else {
            check(putEditTestItem, {
                'Status Response OK': (response) => response.status === 200,
                'Body status response': (response) => response.json().status === true,
                'Success operation': (response) => response.json().message.includes('successfully'),
                'Validate the edited Test item code': (response) => response.json().data.code === bodyForTest.code,
                'Validate the edited Test item name': (response) => response.json().data.name === bodyForTest.name,
            });

            let validateEditionTestItem = http.get(BASE_URL_DEV + variables.endPoint.getTest, { headers: headers })
            let x = JSON.parse(validateEditionTestItem.body)
            let lastItem = x.data.length - 1

            if (!(validateEditionTestItem.status === 200)) {
                fail('Unexpected response: ' + validateEditionTestItem.status);
            } else {
                check(validateEditionTestItem, {
                    'Test item code edited successfully': (response) => response.json().data[lastItem].code === bodyForTest.code,
                    'Test item name edited successfully': (response) => response.json().data[lastItem].name === bodyForTest.name,
                })
            }
        }
    })

    describe('Scenario 4: Successful deleting of a Test item', () => {
        let deleteTestItem = http.del(BASE_URL_DEV + variables.endPoint.deleteTest + idCode, null, { headers: headers })
        if (!(deleteTestItem.status === 200)) {
            fail('Unexpected response: ' + deleteTestItem.status);
        } else {
            check(deleteTestItem, {
                'Status response': (response) => response.status === 200,
                'Body status response': (response) => response.json().status === true,
                'Success Test item deleting': (response) => response.json().message.includes('successfully'),
                'Data deleted': (response) => response.json().data === true
            })
            let validateTestitemDeleted = http.get(BASE_URL_DEV + variables.endPoint.getTest,
                { headers: headers });
            check(validateTestitemDeleted, {
                'Validate that Test item was deleted': (response) => {
                    return response.json().data.every((object) => object.id !== idCode);
                }
            })
        }
    });

    describe('Scenario 5: Successful paginator of Test item', () => {
        const bodyForPaginator = {
            "count": (Math.floor(Math.random() * 2) + 1),
            "page": (Math.floor(Math.random() * 3) + 1),
            "operator": 1,
            "filtersPaginate": [
                {
                    "property": "",
                    "code": ""
                }
            ]
        }

        let paginatorTestitem = http.post(BASE_URL_DEV + variables.endPoint.paginatorG, JSON.stringify(bodyForPaginator),
            { headers: headers })
        if (!(paginatorTestitem.status === 200)) {
            fail('Unexpected response: ' + paginatorTestitem.status);
        } else {
            check(paginatorTestitem, {
                'Status response': (response) => response.status === 200,
                'Body status response': (response) => response.json().status === true,
                'Success operation': (response) => response.json().message.includes('successfully'),
                'Paginator count is the same of the body': (response) => response.json().data.count === bodyForPaginator.count,
                'Paginator page is the same of the body': (response) => response.json().data.page === bodyForPaginator.page
            })
        }
    });

    describe('Scenario 6: Successful search property of Test item', () => {
        let searchPropertyTestitem = http.get(BASE_URL_DEV + variables.endPoint.searchG + "Id/data/" + idItem[x],
            { headers: headers })
        if (!(searchPropertyTestitem.status === 200)) {
            fail('Unexpected response: ' + searchPropertyTestitem.status);
        } else {
            check(searchPropertyTestitem, {
                'Status response': (response) => response.status === 200,
                'Body status response': (response) => response.json().status === true,
                'Validate the Id in the response': (response) => response.json().data.id === idItem[x],
                'Validate the name in the response': (response) => response.json().data.name === nameItem[x],
                'Validate the code in the response': (response) => response.json().data.code === codeItem[x],
            })
        }
    });

    describe('Scenario 7: Successful search list property of Test item', () => {
        let searchListPropertyTestitem = http.get(BASE_URL_DEV + variables.endPoint.searchListG + "Id/data/" + idItem[x],
            { headers: headers })
        if (!(searchListPropertyTestitem.status === 200)) {
            fail('Unexpected response: ' + searchListPropertyTestitem.status);
        } else {
            check(searchListPropertyTestitem, {
                'Status response': (response) => response.status === 200,
                'Body status response': (response) => response.json().status === true,
                'Validate the Id in the response': (response) => response.json().data[0].id === idItem[x],
                'Validate the name in the response': (response) => response.json().data[0].name === nameItem[x],
                'Validate the code in the response': (response) => response.json().data[0].code === codeItem[x],
            })
        }
    });
}
