# Projeto exemplo conexão com Ldap

Exemplo de uso:

Referenciar a DLL ou projeto

var Usuario = "TESTE";
var Senha = "TESTE";
var adAuth = new LdapAuthentication(Usuario, Senha);

try
{
 
	var isAutenticado = adAuth.IsAuthenticated();
	
	if (isAutenticado.Success)
	{
		//Sucesso
	}else{
		//Falha acessar var isAutenticado.Message;
	}
}
catch (Exception ex)
{
}