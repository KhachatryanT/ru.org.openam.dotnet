# ru.org.openam.dotnet
OpenAM .Net SDK and IIS policy agent

[![Build Status](https://travis-ci.org/openam-org-ru/ru.org.openam.dotnet.svg)](https://travis-ci.org/openam-org-ru/ru.org.openam.dotnet)

## Установка и настройка
Идентифицируйте папку ${site}, в которой размещены файлы вашего приложения, путем поиска файла ${site}/web.config

### Установка файлов бинарной поставки:
*  Скачайте файлы бинарной поставки по ссылке http://repo.openam.org.ru/ru.org.openam.iis.httpmodule.zip
*  Распакуйте содержимое в папку ${site}/bin

### Настройка записи журналов полиси агента: 
* Создайте папку ${site}/App_Data/Logs
* Предоставьте право записи для пользователя IUSER_XXX в папку ${site}/App_Data/Logs

### Настройка приложения:
Настройки полиси агента хранятся в файле ${site}/web.config в секции \<appSettings\>, добавьте следующие настройки:
*  \<add key="com.sun.identity.agents.config.naming.url" value="" /\>
*  \<add key="com.sun.identity.agents.config.organization.name" value="/" /\>
*  \<add key="com.sun.identity.agents.app.username" value="" /\>
*  \<add key="com.iplanet.am.service.password" value="" /\>
*  \<add key="com.sun.identity.agents.config.key" value="" /\> (опустите настройку, если password не криптован)
*  \<add key="com.sun.identity.agents.config.local.log.path" value="${basedir}/App_Data/Logs"/\> (переопределите путь хранения журналов)

Значения настроек предоставляются администратором сервера OpenAM или могут быть найдены в файлe c:\iis7_agent\Identifier_${site_id}\config\OpenSSOAgentBootstrap.properties предыдущей установки

### Включение полиси агента:
Включение полиси агента производится в файле ${site}/web.config в секции \<httpModules\> :
* Удалите предыдущую версию полиси агента:  \<add name="iis7agent" /\>
* Добавьте новую версию полиси агента путем добавления строки:  \<add name="OpenAM" type="ru.org.openam.iis.OpenAMHttpModule"\>
* Проверьте работу приложения и файлы журналов в ${site}/App_Data/Logs

ВАЖНО: добавление необходимо производить первой строчкой после тэга  \<httpModules\> или после тега  \<clear/\> внутри \<httpModules\> , если он существует.

### Выключение полиси агента:
Включение полиси агента производится в файле ${site}/web.config в секции \<httpModules\> :
* Удалите строку:  \<add name="OpenAM" type="ru.org.openam.iis.OpenAMHttpModule"\>

### Примеры настройки
Пример настройки ${site}/web.config: https://github.com/openam-org-ru/ru.org.openam.dotnet/blob/master/ru.org.openam.iis.site.sample/web.config

### Возможные проблемы

#### System.Net.WebException: The underlying connection was closed: Could not establish trust relationship for the SSL/TLS secure channel
На сервере используется не доверенный сертификат. Добавьте сертификат сервера в список надежных или отключите строгую проверку сертификатов (не рекомендуется в продуктивной среде) настройкой:

\<add key="com.sun.identity.agents.config.trust.server.certs" value="true"/\>
 
