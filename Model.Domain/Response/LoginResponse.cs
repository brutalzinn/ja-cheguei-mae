namespace api_ja_cheguei_mae.Response
{
    public class LoginResponse
    {

        public bool Status { get; set; } = false;
        public string Token { get; set; }
        public string Email { get; set; } = "DEFAULT";


    }
}
