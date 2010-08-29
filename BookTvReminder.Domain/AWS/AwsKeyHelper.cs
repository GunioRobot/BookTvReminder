using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BookTvReminder.Domain.AWS
{

  //NOTE: AHT - This AWS integration is in the very earliest stages.

  public class AwsKeyHelper
  {
    private string _secretKey;
    public virtual string GetAwsSecretKey(string path = @"e:\aws_secret_key.txt")
    {
      if (string.IsNullOrEmpty(_secretKey))
      {
        _secretKey = File.ReadAllText(path);
      }

      return _secretKey;
    }

    private string _accessKeyId;
    public virtual string GetAwsAccessKeyId(string path = @"e:\aws_access_key_id.txt")
    {
      if (string.IsNullOrEmpty(_accessKeyId))
      {
        _accessKeyId = File.ReadAllText(path);
      }

      return _accessKeyId;
    }

  }
}
