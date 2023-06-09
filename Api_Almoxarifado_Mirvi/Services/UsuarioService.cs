﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Api_Almoxarifado_Mirvi.Data.Dtos;
using Api_Almoxarifado_Mirvi.Models;

namespace Api_Almoxarifado_Mirvi.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task CadastraUsuario(CreateDto dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);

            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

            if(!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuario!");
            }
        }

        public async Task<string> Login(LoginDto dto)
        {
            var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if (!resultado.Succeeded)
            {
                throw new ApplicationException("Usuario nao autenticado!");
            }

            var usuario = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

            var token = _tokenService.GenerateToken(usuario);

            return token;
        }
    }
}
