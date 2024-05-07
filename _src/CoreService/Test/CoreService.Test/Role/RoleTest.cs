using CoreService.Test.Mock.Role;
using CoreService.Test.Ordering;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace CoreService.Test.Role
{
    [TestCaseOrderer("CoreService.Test.Ordering.PriorityOrderer", "CoreService.Test")]
    public sealed class RoleTest(CreateWebApplicationFactory webApplicationFactory) : BaseGlobalTest(webApplicationFactory)
    {
        [Fact, TestPriority(1)]
        public async Task Should_Return_200_When_Role_Created()
        {
            var request = new CreateSingleRoleRequestMock()
            {
                Description = "admin",
                Level = 1,
                Name = "admin",
                ShortCode = "ADM"
            };

            var response = await _httpClient.PostAsJsonAsync("api/v1/roles/create-single-role", request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<CreateSingleRoleResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.ErrorMessage.Should().BeNull();
            parsedResponse?.IsSuccessful.Should().BeTrue();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, TestPriority(2)]
        public async Task Should_Return_400_When_Role_Created_Again_With_Same_Name()
        {
            var request = new CreateSingleRoleRequestMock()
            {
                Description = "admin",
                Level = 2,
                Name = "admin",
                ShortCode = "ADM2"
            };

            var response = await _httpClient.PostAsJsonAsync("api/v1/roles/create-single-role", request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<CreateSingleRoleResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.ErrorMessage.Should().NotBeNullOrEmpty();
            parsedResponse?.IsSuccessful.Should().BeFalse();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact, TestPriority(3)]
        public async Task Should_Return_400_When_Role_Created_Again_With_Same_Level()
        {
            var request = new CreateSingleRoleRequestMock()
            {
                Description = "admin",
                Level = 1,
                Name = "admin2",
                ShortCode = "ADM2"
            };

            var response = await _httpClient.PostAsJsonAsync("api/v1/roles/create-single-role", request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<CreateSingleRoleResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.ErrorMessage.Should().NotBeNullOrEmpty();
            parsedResponse?.IsSuccessful.Should().BeFalse();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact, TestPriority(4)]
        public async Task Should_Return_400_When_Role_Created_Again_With_Same_Short_Code()
        {
            var request = new CreateSingleRoleRequestMock()
            {
                Description = "admin",
                Level = 2,
                Name = "admin2",
                ShortCode = "ADM"
            };

            var response = await _httpClient.PostAsJsonAsync("api/v1/roles/create-single-role", request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<CreateSingleRoleResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.ErrorMessage.Should().NotBeNullOrEmpty();
            parsedResponse?.IsSuccessful.Should().BeFalse();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact, TestPriority(5)]
        public async Task Should_Return_200_When_Roles_Retrieved()
        {
            var response = await _httpClient.GetAsync("api/v1/roles/get-all-roles-without-relation");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<GetAllRolesResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload?.Count.Should().BeGreaterThan(0);
            parsedResponse?.ErrorMessage.Should().BeNull();
            parsedResponse?.IsSuccessful.Should().BeTrue();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, TestPriority(6)]
        public async Task Should_Return_200_When_Role_Retrieved_By_Id()
        {
            var allRolesResponse = await _httpClient.GetAsync("api/v1/roles/get-all-roles-without-relation");
            var allRolesResponseToString = await allRolesResponse.Content.ReadAsStringAsync();
            var parsedAllRolesResponse = JsonSerializer.Deserialize<GetAllRolesResponseMock>(allRolesResponseToString, _jsonSerializerOptions);
            var request = new GetSingleRoleByIdRequestMock() { Id = parsedAllRolesResponse?.Payload[0]?.Id ?? Guid.Empty };
            var response = await _httpClient.PostAsJsonAsync("api/v1/roles/get-single-role-by-id", request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<GetSingleRoleByIdResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload.Should().NotBeNull();
            parsedResponse?.ErrorMessage.Should().BeNull();
            parsedResponse?.IsSuccessful.Should().BeTrue();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, TestPriority(6)]
        public async Task Should_Return_400_When_Role_Retrieved_By_Empty_Id()
        {
            var request = new GetSingleRoleByIdRequestMock() { Id = Guid.Empty };
            var response = await _httpClient.PostAsJsonAsync("api/v1/roles/get-single-role-by-id", request);
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseToString = await response.Content.ReadAsStringAsync();
            responseToString.Should().NotBeNullOrEmpty();
            var parsedResponse = JsonSerializer.Deserialize<GetSingleRoleByEmptyIdResponseMock>(responseToString, _jsonSerializerOptions);
            parsedResponse.Should().NotBeNull();
            parsedResponse?.Payload.Should().BeNull();
            parsedResponse?.ErrorMessage.Should().NotBeNull();
            parsedResponse?.IsSuccessful.Should().BeFalse();
            parsedResponse?.RequestId.Should().NotBeNullOrEmpty();
            parsedResponse?.ServerTime.Should().NotBe(0);
            parsedResponse?.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}