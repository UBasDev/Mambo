using CoreService.Test.Mock.Role;
using CoreService.Test.Ordering;
using Elastic.Apm.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Text.Json;
using CoreService.Test.Mock.User;

namespace CoreService.Test.User
{
    [TestCaseOrderer("CoreService.Test.Ordering.PriorityOrderer", "CoreService.Test")]
    public sealed class UserTest(CreateWebApplicationFactory webApplicationFactory) : BaseGlobalTest(webApplicationFactory)
    {
        private const string _testUserEmail = "john.garter@gmail.com";
        private const string _testUserUsername = "john_garter";
        private const string _testUserPassword = "John1John1John1";

        [Fact, TestPriority(1)]
        public async Task Should_Return_200_When_User_Created()
        {
            var requestToCreateDefaultRole = new CreateSingleRoleRequestMock()
            {
                Description = "admin",
                Level = 10,
                Name = "admin",
                ShortCode = "ADM"
            };
            var responseFromCreatingDefaultRole = await _httpClient.PostAsJsonAsync("api/v1/roles/create-single-role", requestToCreateDefaultRole);
            responseFromCreatingDefaultRole.StatusCode.Should().Be(HttpStatusCode.OK);
            var request = new CreateSingleUserRequestMock()
            {
                Firstname = "john",
                Lastname = "Garter",
                Email = _testUserEmail,
                Username = _testUserUsername,
                Password = _testUserPassword,
                CompanyName = "test_company1"
            };
            var response = await _httpClient.PostAsJsonAsync("api/v1/users/create-single-user", request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<CreateSingleUserResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.ErrorMessage.Should().BeNull();
            parsedResponse?.IsSuccessful.Should().BeTrue();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, TestPriority(2)]
        public async Task Should_Return_400_When_User_Created_With_Same_Email()
        {
            var request = new CreateSingleUserRequestMock()
            {
                Firstname = "john2",
                Lastname = "Garter2",
                Email = _testUserEmail,
                Username = "john2",
                Password = "John2John2John2",
                CompanyName = "test_company2"
            };
            var response = await _httpClient.PostAsJsonAsync("api/v1/users/create-single-user", request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<CreateSingleUserResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.ErrorMessage.Should().NotBeNullOrEmpty();
            parsedResponse?.IsSuccessful.Should().BeFalse();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact, TestPriority(3)]
        public async Task Should_Return_400_When_User_Created_With_Same_Username()
        {
            var request = new CreateSingleUserRequestMock()
            {
                Firstname = "john3",
                Lastname = "Garter3",
                Email = "john.garter3@gmail.com",
                Username = _testUserUsername,
                Password = "John3John3John3",
                CompanyName = "test_company3"
            };
            var response = await _httpClient.PostAsJsonAsync("api/v1/users/create-single-user", request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<CreateSingleUserResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.ErrorMessage.Should().NotBeNullOrEmpty();
            parsedResponse?.IsSuccessful.Should().BeFalse();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact, TestPriority(4)]
        public async Task Should_Return_200_When_User_Logged_In_With_Email()
        {
            var request = new SignInRequestMock()
            {
                EmailOrUsername = _testUserEmail,
                Password = _testUserPassword
            };
            var response = await _httpClient.PostAsJsonAsync("api/v1/users/sign-in", request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<SignInResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload.Should().NotBeNull();
            parsedResponse?.ErrorMessage.Should().BeNull();
            parsedResponse?.IsSuccessful.Should().BeTrue();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.OK);

            parsedResponse?.Payload?.Id.Should().NotBeEmpty();
            parsedResponse?.Payload?.CompanyName.Should().NotBeEmpty();
            parsedResponse?.Payload?.Email.Should().NotBeEmpty();
            parsedResponse?.Payload?.Firstname.Should().NotBeEmpty();
            parsedResponse?.Payload?.Lastname.Should().NotBeEmpty();
            parsedResponse?.Payload?.Username.Should().NotBeEmpty();
        }

        [Fact, TestPriority(5)]
        public async Task Should_Return_200_When_User_Logged_In_With_Username()
        {
            var request = new SignInRequestMock()
            {
                EmailOrUsername = _testUserUsername,
                Password = _testUserPassword
            };
            var response = await _httpClient.PostAsJsonAsync("api/v1/users/sign-in", request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<SignInResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload.Should().NotBeNull();
            parsedResponse?.ErrorMessage.Should().BeNull();
            parsedResponse?.IsSuccessful.Should().BeTrue();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.OK);

            parsedResponse?.Payload?.Id.Should().NotBeEmpty();
            parsedResponse?.Payload?.CompanyName.Should().NotBeEmpty();
            parsedResponse?.Payload?.Email.Should().NotBeEmpty();
            parsedResponse?.Payload?.Firstname.Should().NotBeEmpty();
            parsedResponse?.Payload?.Lastname.Should().NotBeEmpty();
            parsedResponse?.Payload?.Username.Should().NotBeEmpty();
        }

        [Fact, TestPriority(6)]
        public async Task Should_Return_400_When_User_Used_Wrong_Email_To_Log_In()
        {
            var request = new SignInRequestMock()
            {
                EmailOrUsername = "john.garter1@gmail.com",
                Password = _testUserPassword
            };
            var response = await _httpClient.PostAsJsonAsync("api/v1/users/sign-in", request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<SignInResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.ErrorMessage.Should().NotBeNullOrEmpty();
            parsedResponse?.IsSuccessful.Should().BeFalse();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact, TestPriority(7)]
        public async Task Should_Return_400_When_User_Used_Wrong_Username_To_Log_In()
        {
            var request = new SignInRequestMock()
            {
                EmailOrUsername = "john_garter1",
                Password = _testUserPassword
            };
            var response = await _httpClient.PostAsJsonAsync("api/v1/users/sign-in", request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<SignInResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.ErrorMessage.Should().NotBeNullOrEmpty();
            parsedResponse?.IsSuccessful.Should().BeFalse();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact, TestPriority(8)]
        public async Task Should_Return_400_When_User_Used_Wrong_Password_To_Log_In()
        {
            var request = new SignInRequestMock()
            {
                EmailOrUsername = _testUserEmail,
                Password = "wrongPassword1"
            };
            var response = await _httpClient.PostAsJsonAsync("api/v1/users/sign-in", request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<SignInResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.ErrorMessage.Should().NotBeNullOrEmpty();
            parsedResponse?.IsSuccessful.Should().BeFalse();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}