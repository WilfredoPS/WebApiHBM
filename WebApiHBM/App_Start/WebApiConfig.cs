using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApiHBM
{
    public static class WebApiConfig 
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "Authentication",
                routeTemplate: "api/Authentication",
                defaults: new { Controller = "Authentication", Action = "Auth" }
            );

            //Sife Actividades 
            config.Routes.MapHttpRoute(
                name: "ObtenerActividadSife",
                routeTemplate: "api/ObtenerActividadSife",
                defaults: new { Controller = "Sife", Action = "ObtenerActividadSife" }
            ); 

            //Sife HistorialFactura 
            config.Routes.MapHttpRoute(
                name: "ObtenerHistorialFacturas",
                routeTemplate: "api/ObtenerHistorialFacturas",
                defaults: new { Controller = "Sife", Action = "ObtenerHistorialFacturas" }
            );

            //ObtenerUsuarios 
            config.Routes.MapHttpRoute(
                name: "ObtenerUsuarios",
                routeTemplate: "api/ObtenerUsuarios",
                defaults: new { Controller = "Authentication", Action = "ObtenerUsuarios" }
            );

            //ObtenerUsuarios02 
            config.Routes.MapHttpRoute(
                name: "ObtenerUsuarios02",
                routeTemplate: "api/ObtenerUsuarios02",
                defaults: new { Controller = "Authentication", Action = "ObtenerUsuarios02" }
            );

            //ObteneModulo 
            config.Routes.MapHttpRoute(
                name: "ObtenerModulo",
                routeTemplate: "api/ObtenerModulo",
                defaults: new { Controller = "Authentication", Action = "ObtenerModulo" }
            );
            //ObteneParamActividad
            config.Routes.MapHttpRoute(
                name: "ObtenerParamActividad",
                routeTemplate: "api/ObtenerParamActividad",
                defaults: new { Controller = "Authentication", Action = "ObtenerParamActividad" }
            );

            //ObteneModulo 
            config.Routes.MapHttpRoute(
                name: "ObtenerRol",
                routeTemplate: "api/ObtenerRol",
                defaults: new { Controller = "Authentication", Action = "ObtenerRol" }
            );
            //ObteneModulo 
            config.Routes.MapHttpRoute(
                name: "ObtenerRolModulo",
                routeTemplate: "api/ObtenerRolModulo",
                defaults: new { Controller = "Authentication", Action = "ObtenerRolModulo" }
            );
            //Obtener Tablas 
            config.Routes.MapHttpRoute(
                name: "ObtenerTablas",
                routeTemplate: "api/ObtenerTablas",
                defaults: new { Controller = "Authentication", Action = "ObtenerTablas" }
            );
            //Obtener Tablas 
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerTablas02",
                routeTemplate: "api/ObtenerTablas02",
                defaults: new { Controller = "Authentication", Action = "ObtenerTablas02" }
            );
            //Obtener Permisos de Formularios
            config.Routes.MapHttpRoute(
                name: "ObtenerPermisoFormulario",
                routeTemplate: "api/ObtenerPermisoFormulario",
                defaults: new { Controller = "Authentication", Action = "ObtenerPermisoFormulario" }
            );
            //Obtener Maximos de Documentos
            config.Routes.MapHttpRoute(
                name: "ObtenerNroDocumentos",
                routeTemplate: "api/ObtenerNroDocumentos",
                defaults: new { Controller = "Authentication", Action = "ObtenerNroDocumentos" }
            );
            //ObtenerRecurso
            config.Routes.MapHttpRoute(
               name: "ObtenerRecurso",
               routeTemplate: "api/ObtenerRecurso",
               defaults: new { Controller = "Authentication", Action = "ObtenerRecurso" }
            );
            //ObtenerRolRecurso
            config.Routes.MapHttpRoute(
               name: "ObtenerRolRecurso",
               routeTemplate: "api/ObtenerRolRecurso",
               defaults: new { Controller = "Authentication", Action = "ObtenerRolRecurso" }
            );

            config.Routes.MapHttpRoute(
    name: "ObtenerFechaHora",
    routeTemplate: "api/ObtenerFechaHora",
    defaults: new { Controller = "Authentication", Action = "ObtenerFechaHora" }
);

            //ObtenerProductos 
            config.Routes.MapHttpRoute(
                name: "ObtenerProductos",
                routeTemplate: "api/ObtenerProductos",
                defaults: new { Controller = "Orders", Action = "ObtenerProductos" } 
            );
            //ObtenerProductos02 
            config.Routes.MapHttpRoute(
                name: "ObtenerProductos02",
                routeTemplate: "api/ObtenerProductos02",
                defaults: new { Controller = "Orders", Action = "ObtenerProductos02" }
            );
            //ObtenerProductos03 
            config.Routes.MapHttpRoute(
                name: "ObtenerProductos03",
                routeTemplate: "api/ObtenerProductos03",
                defaults: new { Controller = "Orders", Action = "ObtenerProductos03" }
            );
            //ObtenerProductoLote 
            config.Routes.MapHttpRoute(
                name: "ObtenerProductoLote",
                routeTemplate: "api/ObtenerProductoLote",
                defaults: new { Controller = "Orders", Action = "ObtenerProductoLote" }
            );
            //ObtenerProductoLote02
            config.Routes.MapHttpRoute(
                name: "ObtenerProductoLote02",
                routeTemplate: "api/ObtenerProductoLote02",
                defaults: new { Controller = "Orders", Action = "ObtenerProductoLote02" }
            );
            //ObtenerRegion 
            config.Routes.MapHttpRoute(
                name: "ObtenerRegion",
                routeTemplate: "api/ObtenerRegion",
                defaults: new { Controller = "Orders", Action = "ObtenerRegion" }
            );
            //ObtenerCanal 
            config.Routes.MapHttpRoute(
                name: "ObtenerCanal",
                routeTemplate: "api/ObtenerCanal",
                defaults: new { Controller = "Orders", Action = "ObtenerCanal" }
            );
            //ObtenerDivision 
            config.Routes.MapHttpRoute(
                name: "ObtenerDivision",
                routeTemplate: "api/ObtenerDivision",
                defaults: new { Controller = "Orders", Action = "ObtenerDivision" }
            );
            //ObtenerDivision 
            config.Routes.MapHttpRoute(
                name: "ObtenerFamilia",
                routeTemplate: "api/ObtenerFamilia",
                defaults: new { Controller = "Orders", Action = "ObtenerFamilia" }
            );
            //ObtenerSector 
            config.Routes.MapHttpRoute(
                name: "ObtenerSector",
                routeTemplate: "api/ObtenerSector",
                defaults: new { Controller = "Orders", Action = "ObtenerSector" }
            );
            //ObtenerSector02 
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerSector02",
                routeTemplate: "api/ObtenerSector02",
                defaults: new { Controller = "Orders", Action = "ObtenerSector02" }
            );
            //ObtenerGrupo 
            config.Routes.MapHttpRoute(
                name: "ObtenerGrupo",
                routeTemplate: "api/ObtenerGrupo",
                defaults: new { Controller = "Orders", Action = "ObtenerGrupo" }
            );
            // ObtenerGrupo02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerGrupo02",
                routeTemplate: "api/ObtenerGrupo02",
                defaults: new { Controller = "Orders", Action = "ObtenerGrupo02" }
            );
            //ObtenerSubGrupo 
            config.Routes.MapHttpRoute(
                name: "ObtenerSubGrupo",
                routeTemplate: "api/ObtenerSubGrupo",
                defaults: new { Controller = "Orders", Action = "ObtenerSubGrupo" }
            );
            // ObtenerSubGrupo02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerSubGrupo02",
                routeTemplate: "api/ObtenerSubGrupo02",
                defaults: new { Controller = "Orders", Action = "ObtenerSubGrupo02" }
            );
            //ObtenerOficinaVentas
            config.Routes.MapHttpRoute(
                name: "ObtenerOficinaVenta",
                routeTemplate: "api/ObtenerOficinaVenta",
                defaults: new { Controller = "Orders", Action = "ObtenerOficinaVenta" }
            );
            //ObtenerBonificacionCab
            config.Routes.MapHttpRoute(
                name: "ObtenerBonificacionCab",
                routeTemplate: "api/ObtenerBonificacionCab",
                defaults: new { Controller = "Orders", Action = "ObtenerBonificacionCab" }
            );
            // ObtenerBonificacionCab02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerBonificacionCab02",
                routeTemplate: "api/ObtenerBonificacionCab02",
                defaults: new { Controller = "Orders", Action = "ObtenerBonificacionCab02" }
            );

            //ObtenerProdConBono
            config.Routes.MapHttpRoute(
                name: "ObtenerProdConBono",
                routeTemplate: "api/ObtenerProdConBono",
                defaults: new { Controller = "Orders", Action = "ObtenerProdConBono" }
            );
            // ObtenerProdConBono02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerProdConBono02",
                routeTemplate: "api/ObtenerProdConBono02",
                defaults: new { Controller = "Orders", Action = "ObtenerProdConBono02" }
            );
            //ObtenerProdParaBono
            config.Routes.MapHttpRoute(
                name: "ObtenerProdParaBono",
                routeTemplate: "api/ObtenerProdParaBono",
                defaults: new { Controller = "Orders", Action = "ObtenerProdParaBono" }
            );
            // ObtenerProdParaBono02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerProdParaBono02",
                routeTemplate: "api/ObtenerProdParaBono02",
                defaults: new { Controller = "Orders", Action = "ObtenerProdParaBono02" }
            );

            //ObtenerProductoAlternativa
            config.Routes.MapHttpRoute(
                name: "ObtenerProductoAlternativa",
                routeTemplate: "api/ObtenerProductoAlternativa",
                defaults: new { Controller = "Orders", Action = "ObtenerProductoAlternativa" }
            );
            //ObtenerReglaPedido
            config.Routes.MapHttpRoute(
                name: "ObtenerReglaPedido",
                routeTemplate: "api/ObtenerReglaPedido",
                defaults: new { Controller = "Orders", Action = "ObtenerReglaPedido" }
            );
            // ObtenerReglaPedido02            
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerReglaPedido02",
                routeTemplate: "api/ObtenerReglaPedido02",
                defaults: new { Controller = "Orders", Action = "ObtenerReglaPedido02" }
            );
            //ObtenerTipoPago
            config.Routes.MapHttpRoute(
                name: "ObtenerTipoPago",
                routeTemplate: "api/ObtenerTipoPago",
                defaults: new { Controller = "Orders", Action = "ObtenerTipoPago" }
            );
            //ObtenerPrioridadEntrega
            config.Routes.MapHttpRoute(
                name: "ObtenerPrioridadEntrega",
                routeTemplate: "api/ObtenerPrioridadEntrega",
                defaults: new { Controller = "Orders", Action = "ObtenerPrioridadEntrega" }
            );
            //ObtenerPlanZona
            config.Routes.MapHttpRoute(
                name: "ObtenerPlanZona",
                routeTemplate: "api/ObtenerPlanZona",
                defaults: new { Controller = "Orders", Action = "ObtenerPlanZona" }
            );
            // ObtenerPlanZona02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerPlanZona02",
                routeTemplate: "api/ObtenerPlanZona02",
                defaults: new { Controller = "Orders", Action = "ObtenerPlanZona02" }
            );
            //ObtenerDestinatarios
            config.Routes.MapHttpRoute(
                name: "ObtenerDestinatarios",
                routeTemplate: "api/ObtenerDestinatarios",
                defaults: new { Controller = "Orders", Action = "ObtenerDestinatarios" }
            );
            // ObtenerDestinatarios02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerDestinatarios02",
                routeTemplate: "api/ObtenerDestinatarios02",
                defaults: new { Controller = "Orders", Action = "ObtenerDestinatarios02" }
            );
            //ObtenerTipoPedido
            config.Routes.MapHttpRoute(
                name: "ObtenerTipoPedido",
                routeTemplate: "api/ObtenerTipoPedido",
                defaults: new { Controller = "Orders", Action = "ObtenerTipoPedido" }
            );
            //ObtenerUsuarioSector
            config.Routes.MapHttpRoute(
                name: "ObtenerUsuarioSector",
                routeTemplate: "api/ObtenerUsuarioSector",
                defaults: new { Controller = "Orders", Action = "ObtenerUsuarioSector" }
            );
            //ObtenerUsuarioAlmacen
            config.Routes.MapHttpRoute(
                name: "ObtenerUsuarioAlmacen",
                routeTemplate: "api/ObtenerUsuarioAlmacen",
                defaults: new { Controller = "Orders", Action = "ObtenerUsuarioAlmacen" }
            );
            //ObtenerUsuarioSector
            config.Routes.MapHttpRoute(
                name: "ObtenerPedido",
                routeTemplate: "api/ObtenerPedido",
                defaults: new { Controller = "Orders", Action = "ObtenerPedido" }
            );
            // ObtenerPedido02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerPedido02",
                routeTemplate: "api/ObtenerPedido02",
                defaults: new { Controller = "Orders", Action = "ObtenerPedido02" }
            );

            //ObtenerUsuarioSector
            config.Routes.MapHttpRoute(
                name: "ObtenerPedidoDetalle",
                routeTemplate: "api/ObtenerPedidoDetalle",
                defaults: new { Controller = "Orders", Action = "ObtenerPedidoDetalle" }
            );
            //ObtenerPedidoDetalle02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerPedidoDetalle02",
                routeTemplate: "api/ObtenerPedidoDetalle02",
                defaults: new { Controller = "Orders", Action = "ObtenerPedidoDetalle02" }
            );
            //ObtenerRamp
            config.Routes.MapHttpRoute(
                name: "ObtenerRamo",
                routeTemplate: "api/ObtenerRamo",
                defaults: new { Controller = "Orders", Action = "ObtenerRamo" }
            );
            // ObtenerRamo02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerRamo02",
                routeTemplate: "api/ObtenerRamo02",
                defaults: new { Controller = "Orders", Action = "ObtenerRamo02" }
            );
            //ObtenerSubRamo
            config.Routes.MapHttpRoute(
                name: "ObtenerSubRamo",
                routeTemplate: "api/ObtenerSubRamo",
                defaults: new { Controller = "Orders", Action = "ObtenerSubRamo" }
            );
            // ObtenerSubRamo02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerSubRamo02",
                routeTemplate: "api/ObtenerSubRamo02",
                defaults: new { Controller = "Orders", Action = "ObtenerSubRamo02" }
            );
            //ObteneTratamiento
            config.Routes.MapHttpRoute(
                name: "ObtenerTratamiento",
                routeTemplate: "api/ObtenerTratamiento",
                defaults: new { Controller = "Orders", Action = "ObtenerTratamiento" }
            );

            //ObtenerStock de Productos stock
            config.Routes.MapHttpRoute(
                name: "ProductoStock",
                routeTemplate: "api/ProductoStock",
                defaults: new { Controller = "Pedido", Action = "ProductoStock" }
            );

            //ObtenerCustomers
            config.Routes.MapHttpRoute(
                name: "ObtenerCustomers",
                routeTemplate: "api/ObtenerCustomers",
                defaults: new { Controller = "Customers", Action = "ObtenerCustomers" }
            );
            //ObtenerCustomers02
            config.Routes.MapHttpRoute(
                name: "ObtenerCustomers02",
                routeTemplate: "api/ObtenerCustomers02",
                defaults: new { Controller = "Customers", Action = "ObtenerCustomers02" }
            );
            
            //ObtenerCustomersSector
            config.Routes.MapHttpRoute(
                name: "ObtenerCustomersSector",
                routeTemplate: "api/ObtenerCustomersSector",
                defaults: new { Controller = "Customers", Action = "ObtenerCustomersSector" }
            );
            //ObtenerCustomersSector02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-08
            config.Routes.MapHttpRoute(
                name: "ObtenerCustomersSector02",
                routeTemplate: "api/ObtenerCustomersSector02",
                defaults: new { Controller = "Customers", Action = "ObtenerCustomersSector02" }
            );
            //ObtenerDestinatarios
            config.Routes.MapHttpRoute(
                name: "CustomersSector",
                routeTemplate: "api/CustomersSector",
                defaults: new { Controller = "Customers", Action = "CustomersSector" }
            );
            //ObtenerDestinatarios
            config.Routes.MapHttpRoute(
                name: "ObtenerContactos",
                routeTemplate: "api/ObtenerContactos",
                defaults: new { Controller = "Customers", Action = "ObtenerContactos" }
            );
            //ObtenerContactos02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-08
            config.Routes.MapHttpRoute(
                name: "ObtenerContactos02",
                routeTemplate: "api/ObtenerContactos02",
                defaults: new { Controller = "Customers", Action = "ObtenerContactos02" }
            );
            //ObtenerDestinatarios
            config.Routes.MapHttpRoute(
                name: "ObtenerTipoDocIdentificacion",
                routeTemplate: "api/ObtenerTipoDocIdentificacion",
                defaults: new { Controller = "Customers", Action = "ObtenerTipoDocIdentificacion" }
            );
            //ObtenerDestinatarios
            config.Routes.MapHttpRoute(
                name: "ObtenerRegimenTributario",
                routeTemplate: "api/ObtenerRegimenTributario",
                defaults: new { Controller = "Customers", Action = "ObtenerRegimenTributario" }
            );
            //ObtenerDestinatarios
            config.Routes.MapHttpRoute(
                name: "BusquedaCustomers",
                routeTemplate: "api/BusquedaCustomers",
                defaults: new { Controller = "Customers", Action = "BusquedaCustomers" }
            );
            //ObtenerCentro
            config.Routes.MapHttpRoute(
                name: "ObtenerCentro",
                routeTemplate: "api/ObtenerCentro",
                defaults: new { Controller = "Customers", Action = "ObtenerCentro" }
            );

            //ObtenerActividad
            config.Routes.MapHttpRoute(
                name: "ObtenerActividad",
                routeTemplate: "api/ObtenerActividad",
                defaults: new { Controller = "Activity", Action = "ObtenerActividad" }
            );
            // ObtenerActividad02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-09
            config.Routes.MapHttpRoute(
                name: "ObtenerActividad02",
                routeTemplate: "api/ObtenerActividad02",
                defaults: new { Controller = "Activity", Action = "ObtenerActividad02" }
            );
            //ObtenerActividad
            config.Routes.MapHttpRoute(
                name: "ObtenerTarea",
                routeTemplate: "api/ObtenerTarea",
                defaults: new { Controller = "Activity", Action = "ObtenerTarea" }
            );
            //ObtenerActividad
            config.Routes.MapHttpRoute(
                name: "ObtenerTipoTarea",
                routeTemplate: "api/ObtenerTipoTarea",
                defaults: new { Controller = "Activity", Action = "ObtenerTipoTarea" }
            );
            //ObtenerActividad
            config.Routes.MapHttpRoute(
                name: "ObtenerMotivoNoActividad",
                routeTemplate: "api/ObtenerMotivoNoActividad",
                defaults: new { Controller = "Activity", Action = "ObtenerMotivoNoActividad" }
            );

            //ObtenerBanco
            config.Routes.MapHttpRoute(
                name: "ObtenerBanco",
                routeTemplate: "api/ObtenerBanco",
                defaults: new { Controller = "Cobranzas", Action = "ObtenerBanco" }
            );
            // ObtenerBanco02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-08
            config.Routes.MapHttpRoute(
                name: "ObtenerBanco02",
                routeTemplate: "api/ObtenerBanco02",
                defaults: new { Controller = "Cobranzas", Action = "ObtenerBanco02" }
            );

            config.Routes.MapHttpRoute(
               name: "ObtenerRecibos",
               routeTemplate: "api/ObtenerRecibos",
               defaults: new { Controller = "Cobranzas", Action = "ObtenerRecibos" }
            );
            // ObtenerRecibos02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-08
            config.Routes.MapHttpRoute(
               name: "ObtenerRecibos02",
               routeTemplate: "api/ObtenerRecibos02",
               defaults: new { Controller = "Cobranzas", Action = "ObtenerRecibos02" }
            );

            config.Routes.MapHttpRoute(
               name: "ObtenerAbonos",
               routeTemplate: "api/ObtenerAbonos",
               defaults: new { Controller = "Cobranzas", Action = "ObtenerAbonos" }
           );
            // ObtenerRecibos02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-08
            config.Routes.MapHttpRoute(
               name: "ObtenerAbonos02",
               routeTemplate: "api/ObtenerAbonos02",
               defaults: new { Controller = "Cobranzas", Action = "ObtenerAbonos02" }
            );

            config.Routes.MapHttpRoute(
               name: "ObtenerFacturas",
               routeTemplate: "api/ObtenerFacturas",
               defaults: new { Controller = "Cobranzas", Action = "ObtenerFacturas" }
           );
            // ObtenerFacturas02
            // Desarrollador: Limberg Alcon Espejo
            // Fecha: 2018-10-08
            config.Routes.MapHttpRoute(
               name: "ObtenerFacturas02",
               routeTemplate: "api/ObtenerFacturas02",
               defaults: new { Controller = "Cobranzas", Action = "ObtenerFacturas02" }
           );
            //ObtenerHistorialCobranzas 
            config.Routes.MapHttpRoute(
               name: "ObtenerHistorialCobranzas",
               routeTemplate: "api/ObtenerHistorialCobranzas",
               defaults: new { Controller = "Cobranzas", Action = "ObtenerHistorialCobranzas" }
            );
            //ObtenerStock de Productos stock
            config.Routes.MapHttpRoute(
                name: "RealizarPedido",
                routeTemplate: "api/RealizarPedido",
                defaults: new { Controller = "Pedido", Action = "RealizarPedido" }
            );
            
            
            config.Routes.MapHttpRoute(
                name: "ClienteUbicacion",
                routeTemplate: "api/ClienteUbicacion",
                defaults: new { Controller = "ClienteUbicacion", Action = "ClienteUbicacion" }
            );
            config.Routes.MapHttpRoute(
                name: "ObtenerDescuento",
                routeTemplate: "api/ObtenerDescuento",
                defaults: new { Controller = "Descuento", Action = "ObtenerDescuento" }
            );
            config.Routes.MapHttpRoute(
                name: "ObtenerDetDescuento",
                routeTemplate: "api/ObtenerDetDescuento",
                defaults: new { Controller = "Descuento", Action = "ObtenerDetDescuento" }
            );
            
            config.Routes.MapHttpRoute(
                name: "ObtenerPlanInventario",
                routeTemplate: "api/ObtenerPlanInventario",
                defaults: new { Controller = "Inventarios", Action = "ObtenerPlanInventario" }
            );
            config.Routes.MapHttpRoute(
                name: "ObtenerInventario",
                routeTemplate: "api/ObtenerInventario",
                defaults: new { Controller = "Inventarios", Action = "ObtenerInventario" }
            );
            config.Routes.MapHttpRoute(
                name: "ObtenerparamInventario",
                routeTemplate: "api/ObtenerparamInventario",
                defaults: new { Controller = "Inventarios", Action = "ObtenerparamInventario" }
            );            

            //Obtener Repuestos de Equipos

            config.Routes.MapHttpRoute(
                name: "ObtenerRecepcionEquipos",
                routeTemplate: "api/ObtenerRecepcionEquipos",
                defaults: new { Controller = "Inventarios", Action = "ObtenerRecepcionEquipos" }
            );
            config.Routes.MapHttpRoute(
                name: "ObtenerHistorialRecepcionEquipos",
                routeTemplate: "api/ObtenerHistorialRecepcionEquipos",
                defaults: new { Controller = "Inventarios", Action = "ObtenerHistorialRecepcionEquipos" }
            );
            config.Routes.MapHttpRoute(
                name: "ObtenerRecepcionRegionalTaller",
                routeTemplate: "api/ObtenerRecepcionRegionalTaller",
                defaults: new { Controller = "Inventarios", Action = "ObtenerRecepcionRegionalTaller" }
            );


            config.Routes.MapHttpRoute(
            name: "ObtenerHistoricoSector", 
            routeTemplate: "api/ObtenerHistoricoSector",
            defaults: new { Controller = "Historico", Action = "ObtenerHistoricoSector" }
            );
            config.Routes.MapHttpRoute(
            name: "ObtenerHistoricoPedido",
            routeTemplate: "api/ObtenerHistoricoPedido",
            defaults: new { Controller = "Historico", Action = "ObtenerHistoricoPedido" }
            );
            config.Routes.MapHttpRoute(
               name: "ObtenerDeleteTracking",
               routeTemplate: "api/ObtenerDeleteTracking",
               defaults: new { Controller = "DeleteTracking", Action = "ObtenerDeleteTracking" }
            );
            config.Routes.MapHttpRoute(
               name: "Eliminar_Conexiones",
               routeTemplate: "api/Eliminar_Conexiones",
               defaults: new { Controller = "DeleteTracking", Action = "Eliminar_Conexiones" }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
