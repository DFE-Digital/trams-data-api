import Ajv from 'ajv';
import addFormats from 'ajv-formats'


// error message
const getSchemaError = (getAjvError) => {
  return cy.wrap(
    `Field: ${getAjvError[0]['dataPath']} is invalid. Cause: ${getAjvError[0]['message']}`
  );
};

// commands function
export const validateSchema = (schema, response) => {
  
  const ajv = new Ajv()
  addFormats(ajv)
  const validate = ajv.compile(schema);

  const valid = validate(response);
  console.log(validate.errors)

  if (!valid) {
    getSchemaError(validate.errors).then((schemaError) => {
      throw new Error(schemaError);
    });
  } else {
    cy.log('Schema validated!');
  }
};
