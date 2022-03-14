describe("E2E Academy Transfers", () => {
    let apiKey = Cypress.env('apiKey');
    let url = Cypress.env('url');
    var savedURN;
    var savedOutgoingTrustUkprn;

    it('GET All Academy Transfer Projects', () => {
        cy.request({
            method : 'GET',
            failOnStatusCode: false,
            url: url+"/academytransferproject",
            headers: {
                ApiKey: apiKey,
                "Content-type" : "application/json"
            }
        })
        .then((response) =>{
            expect(response.status).to.eq(200);
            savedURN = response.body[0].projectUrn;
            cy.log("1st Project URN = "+savedURN);
        })
    });

    it.skip('Find and update a Project and verify the changes', () => {
        cy.request({
            method : 'GET',
            failOnStatusCode: false,
            url: url+"/academyTransferProject/"+savedURN,
            headers: {
                ApiKey: apiKey,
                "Content-type" : "application/json"
            }
        })
        .then((response) =>{
            savedOutgoingTrustUkprn = response.body.outgoingTrustUkprn;
            cy.log("savedOutgoingTrustUkprn = "+savedOutgoingTrustUkprn);
            expect(response.status).to.eq(200);
            expect("Project URN = "+response.body.projectUrn).to.eq("Project URN = "+savedURN)
            expect(response.body.academyPerformanceAdditionalInformation)
            .to.eq("Offstead Report")
        })
        
        cy.request({
            method : 'PATCH',
            failOnStatusCode: false,
            body:{
                "outgoingTrustUkprn": "10060936",
                "academyPerformanceAdditionalInformation": "Offstead Report - UPDATE TEST"            
            },
            url: url+"/academyTransferProject/"+savedURN,
            headers: {
                ApiKey: apiKey,
                "Content-type" : "application/json"
            }
        })
        .then((response) =>{
            expect(response.status).to.eq(200);
            expect(response.body.academyPerformanceAdditionalInformation)
            .to.eq("Offstead Report - UPDATE TEST")
        })

        cy.request({
            method : 'PATCH',
            failOnStatusCode: false,
            body:{
                "outgoingTrustUkprn": "10060936",
                "academyPerformanceAdditionalInformation": "Offstead Report"            
            },
            url: url+"/academyTransferProject/"+savedURN,
            headers: {
                ApiKey: apiKey,
                "Content-type" : "application/json"
            }
        })
        .then((response) =>{
            expect(response.status).to.eq(200);
            expect(response.body.academyPerformanceAdditionalInformation)
            .to.eq("Offstead Report")
        })
    });
       
});