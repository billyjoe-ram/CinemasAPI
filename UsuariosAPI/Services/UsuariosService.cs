using AutoMapper;

using UsuariosAPI.Models;
using UsuariosAPI.Exceptions;
using UsuariosAPI.Data.Dtos;

using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Request;

namespace UsuariosAPI.Services
{
    public class UsuariosService
    {
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;
        private EmailService _emailService;
        public UsuariosService(
            IMapper mapper,
            UserManager<IdentityUser<int>> userManager,
            EmailService emailService
        )
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public string CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
            IdentityUser<int> usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);

            Task<IdentityResult> resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, usuarioDto.Senha);

            if (!resultadoIdentity.Result.Succeeded)
            {
                List<string> erros = resultadoIdentity.Result.Errors.Select(e => e.Description).ToList();
                string mensagemDeErro = String.Join("\n", erros);
                throw new RegisterException($"Erro ao cadastrar usuário:\n{mensagemDeErro}");
            }

            var resultadoIdentityCodigo = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity);

            if (!resultadoIdentityCodigo.IsCompletedSuccessfully)
            {
                throw new RegisterException($"Erro ao gerar código de Confirmação Por E-mail");
            }


            string code = resultadoIdentityCodigo.Result;
            EnviarEmailConfirmacao(usuarioIdentity, code);

            return code;
        }

        public void AtivarUsuario(AtivaContaRequest ativaContaRequest)
        {
            var identityUser = _userManager
                .Users
                .First(u => u.Id == ativaContaRequest.UsuarioId);

            var identityResult = _userManager.ConfirmEmailAsync(identityUser, ativaContaRequest.CodigoAtivacao);

            if (!identityResult.Result.Succeeded) { 
                throw new ActiveAccountByEmailException();
            }
        }

        private void EnviarEmailConfirmacao(IdentityUser<int> usuarioIdentity, string tokenAtivacao)
        {
            string destinatario = usuarioIdentity.Email;
            string assunto = "Código de ativação";

            string corpoEmail = $"Olá {usuarioIdentity.UserName}!\n\n";
            corpoEmail += "Aqui está seu código de ativação para o CinemasAPI. ";
            corpoEmail += $"Envie seu id ({usuarioIdentity.Id}) e o token \"{tokenAtivacao}\" ";
            corpoEmail += "para http://localhost:5099/usuarios/ativar e sua conta ficará ativa!";

            _emailService.EnviarEmail(destinatario, assunto, corpoEmail);
        }

    }
}
