using FluentAssertions;
using FizzWare.NBuilder;
using TramsDataApi.DatabaseModels;
using TramsDataApi.Factories;
using TramsDataApi.RequestModels.ApplyToBecome;
using Xunit;

namespace TramsDataApi.Test.Factories
{
    public class A2BApplicationKeyPersonsFactoryTests
    {
	    [Fact]
        public void Create_ReturnsNull_WhenA2BApplicationKeyPersonsIsNull()
        {
            var response = A2BApplicationKeyPersonsFactory.Create(null);

            response.Should().BeNull();
        }

        [Fact]
        public void Create_ReturnsExpectedA2BApplicationKeyPerson_WhenA2BApplicationKeyPersonsResponseIsProvided()
        {
            var keyPersonsCreateRequest = Builder<A2BApplicationKeyPersonsCreateRequest>
                .CreateNew()
                .Build();

            var expectedKeyPersons = new A2BApplicationKeyPersons
            {
	            
            };
                
            var response = A2BApplicationKeyPersonsFactory.Create(keyPersonsCreateRequest);

            response.Should().BeEquivalentTo(expectedKeyPersons);
        }
    }
}