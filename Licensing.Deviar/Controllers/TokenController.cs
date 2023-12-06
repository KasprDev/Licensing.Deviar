using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Licensing.Deviar.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UnitedCoachwaysCRM.Models;

namespace Licensing.Deviar.Controllers
{
    public class TokenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrinFactory;

        public TokenController(
            ApplicationDbContext context,
            SignInManager<AppUser> signInManager,
            IUserClaimsPrincipalFactory<AppUser> userClaimsPrinFactory)
        {
            _context = context;
            _signInManager = signInManager;
            _userClaimsPrinFactory = userClaimsPrinFactory;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="tokenRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/token")]
        public async Task<IActionResult> Token([FromBody] TokenRequestDto tokenRequest)
        {
            var matchingUser = _context.Users.FirstOrDefault(x => x.Email == tokenRequest.Email);

            if (matchingUser == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid email address or password. Please try again."
                });
            }

            var signIn = await _signInManager.CheckPasswordSignInAsync(matchingUser, tokenRequest.Password, true);

            if (!signIn.Succeeded)
                return BadRequest(new
                {
                    Message = "Invalid email address or password. Please try again."
                });

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("JpIpw4UYLkiaQZC2mymLZ82AIa3niKRDVffEyImoDWCI6hOC2Ev7M0d5pFxm"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var stupidClaims = await _userClaimsPrinFactory.CreateAsync(matchingUser);

            var claims = new List<Claim>(stupidClaims.Claims)
                { new Claim("securitystamp", matchingUser.SecurityStamp) };

            var token = new JwtSecurityToken(
                issuer: "licensing.deviar.net",
                audience: "licensing.deviar.net",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: creds);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = token.ValidTo,
                Reseller = matchingUser.Reseller
            });
        }
    }
}