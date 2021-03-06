/// <reference types="Cypress"/>
describe("GET conversion-projects", () => {
 let apiKey = Cypress.env('apiKey');
 let url = Cypress.env('url')
  it("Should return a valid 200 response", () => {
    cy.request({
      failOnStatusCode: false,
      url: url+"/conversion-projects?",
      headers: {
        ApiKey: apiKey,
      }
    }).its('status').should('eq', 200)
  });

  it("Should return a valid 401 response when omitting API key", () => {
    cy.request({
      failOnStatusCode: false,
      url: url+"/conversion-projects?",
      headers: {
        ApiKey: '',
      }
    }).its('status').should('eq', 401)
  });

  it('Should reject invalid \'?count\' parameters - alphabetical chars', () => {
    cy.request({
        failOnStatusCode: false,
        url: url+"/conversion-projects?count=abcdef",
        headers: {
          ApiKey: apiKey,
        }
      })
      .its('body.errors.count').should('contain','The value \'abcdef\' is not valid.')
  });
});
