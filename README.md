# Aplicación ClientCodeFirst

Este proyecto despliega una aplicación ClientCodeFirst desarrollada en .NET Core, junto con su base de datos MySQL. La aplicación expone una API REST para gestionar clientes.

## Arquitectura

La arquitectura consiste en los siguientes componentes:

- **Aplicación ClientCodeFirst**: Se despliega en un Pod con un contenedor Docker basado en una imagen de la propia aplicación. El contenedor expone el puerto 5091 de la API REST. El Pod está gestionado por un controlador Deployment. A su vez, un Service de tipo LoadBalancer expone el Pod públicamente.

- **Base de datos MySQL**: Se despliega en otro Pod con un contenedor Docker oficial de MySQL. Para hacer persistentes los datos, este Pod usa un volumen con storage class HostPath, que monta un directorio del nodo host. Las credenciales de la BD se injectan como secreto de Kubernetes.

## Despliegue

Se requieren los siguientes manifiestos:

**codefirst-deployment.yaml**:  
- Define el Deployment de la app ClientCodeFirst con una réplica. 
- Usa una imagen Docker desde registro local (localhost:5000).
- Inyecta variables de entorno para:
  - Conexión a BD MySQL (host, usuario, contraseña, BD)
  - Puerto de exposición de la API  

**mysql-deployment.yaml**:
- Define el Deployment de MySQL con una réplica.
- Monta un volumen HostPath en /var/lib/mysql para persistencia.   
- Usa un secreto para inyectar las credenciales de BD.
- Expone el puerto 3306 de MySQL en un Service LoadBalancer.

**mysql-secret.yaml**:
- Define las credenciales de BD en forma de secreto de Kubernetes:
  - Usuario y contraseña
  - BD 
  - Password de root
