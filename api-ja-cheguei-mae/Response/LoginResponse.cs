namespace api_ja_cheguei_mae.Response
{
    public class LoginResponse
    {
        public LoginResponse(bool status)
        {
            Status = status;
        }
        public bool Status { get; set; }
        public string Token { get; set; } = "TESTE";
    }
}
