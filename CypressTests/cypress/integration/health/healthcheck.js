describe("Health Check and Database Check", () => {
    let apiKey = Cypress.env('apiKey');
    let url = Cypress.env('url')
    let urn;

    it('Health Check', () => {
        cy.request({
            method : 'GET',
            failOnStatusCode: false,
            url: url+"/HealthCheck",
            headers: {
              ApiKey: apiKey,
              "Content-type" : "application/json"
            }
          })
          .should((response)=>{
            expect(response.body).to.contain('Health check ok');
            expect(response.status).to.eq(200);
            cy.log(JSON.stringify(response.body));
          })
      });

      it('Database Check', () => {
        cy.request({
            method : 'GET',
            failOnStatusCode: false,
            url: url+"/check_db",
            headers: {
              ApiKey: apiKey,
              "Content-type" : "application/json"
            }
          })
          .should((response)=>{ 
            expect(response.status).to.eq(200);
            expect(response.body).to.eq(true);
            cy.log("Database Check = "+JSON.stringify(response.body));
          })
      });

   });