# Sistema de votacion

### Descripcion

Este es un sistema de votación que el cual se basa en lo que es todo el proceso de que se realiza durante la elección de algún candidato de un país. Este sistema contiene dos vertientes una es la parte administrativa y la segunda parte es la que utiliza el ciudadano que va a realizar el voto.

En la parte administrativa el administrador podrá gestionar los partidos, ciudadanos, puestos electivos, candidatos, elecciones y revisar los resultados de las elecciones. Este sistema cuenta con varias validaciones en sus operaciones para la administración. Dentro de las validaciones que podemos mencionar esta que el administrador no puede acceder a la pantalla de votación durante proceso elecciones, regulación en los requisitos para crear elecciones, etc.

En lo que concierne a la pantalla de votaciones el ciudadano podrá ejercer su derecho al voto autenticándose con su documento de identidad, posteriormente seleccionara al candidato de su preferencia con respecto a los diferentes puestos electivos que se están efectuando hasta concluir con todas las posiciones. Luego de que el ciudadano termine su voto, recibirá un correo con los resultados de su votación.


### Validaciones

El sistema cuenta con varias validaciones las cual seran mencionadas a continuancion:

  - Al momento de crear un candidato debe existir al menos un partido y un puesto electivo creado y activo para el candidato ser creado.
  - Cuando el administrado finaliza una elecccion ningún elector podrá seguir votando sobre la misma.
  - Si existe una eleccion activa el administrador no puede crear una nueva.
  - Si existe una adminsitrador logueado en el sistema, si intenta entrar a la pantalla inicial se debe de redirigir a la pantalla de administración.
  - Ningún usuario no autorizado puede acceder a las opciones del administrador, aun este conociera las urls de acceso.
  - Al inactivar un ciudadano este no puede participar en un proceso de eleccion.
  - Al inactivar un candidato es no aparecera para que los ciudadanos puedan votar por el.
  - Al inactivar un partido este no aparecera para ser seleccionado para una eleccion y todos los candidatos que pertenezcan a este son desactivados.
  - No se puede desactivar o activar candidatos, puesto electivo, ciudadanos y partidos si existe un proceso de eleccion abierto.
  - Para los resultados de las elecciones no se toma en cuenta si esta activo o inactivo el candidato o ciudadano siempre y cuando este haya participado en el proceso de eleccion.

### Tecnologias
  
  - ASP Core.
  - Entity Framework.
  - Identity.
  - MailKit.
  - Bootstrap.
  - Razor page.
  - SQL Server.
